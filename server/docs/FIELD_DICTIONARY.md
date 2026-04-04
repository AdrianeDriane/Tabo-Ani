**Access**

**1.) users**
Purpose: Main account record for every platform user.

**Primary Key**

- `user_id uuid pk` - Unique user identifier

**Fields**

- `email varchar(255) null` - User email
- `mobile_number varchar(20) null` - User mobile number
- `password_hash text null` - Hashed password for local auth
- `first_name varchar(100) not null` - Given name
- `last_name varchar(100) not null` - Family name
- `display_name varchar(150) null` - Public-facing display name
- `profile_photo_url text null` - Profile image URL
- `is_email_verified boolean not null default false` - Email verification flag
- `is_mobile_verified boolean not null default false` - Mobile verification flag
- `account_status text not null` - Account state, e.g. ACTIVE, SUSPENDED, DEACTIVATED
- `last_login_at timestamptz null` - Most recent login timestamp
- `created_at timestamptz not null default now()` - Record creation timestamp
- `updated_at timestamptz not null default now()` - Record update timestamp

**Unique Constraints**

- `unique (email)` where email is not null
- `unique (mobile_number)` where mobile_number is not null

**Notes**

- Add a check so at least one of `email` or `mobile_number` exists.

---

**2.) roles**
Purpose: Master list of system roles.

**Primary Key**

- `role_id uuid pk` - Unique role identifier

**Fields**

- `role_code varchar(50) not null` - Stable code, e.g. FARMER, BUYER, DISTRIBUTOR, ADMIN
- `role_name varchar(100) not null` - Human-readable role name
- `description text null` - Short explanation of the role
- `created_at timestamptz not null default now()`

**Unique Constraints**

- `unique (role_code)`
- `unique (role_name)`

---

**3.) user_roles**
Purpose: Junction table allowing one user to have multiple roles.

**Primary Key**

- `user_role_id uuid pk` - Unique user-role assignment identifier

**Foreign Keys**

- `user_id uuid not null fk -> users.user_id`
- `role_id uuid not null fk -> roles.role_id`

**Fields**

- `assigned_at timestamptz not null default now()` - When role was assigned
- `is_active boolean not null default true` - Whether role assignment is active
- `created_at timestamptz not null default now()`

**Unique Constraints**

- `unique (user_id, role_id)`

---

**User Profiles and Verification**

**4.) farmer_profiles**
Purpose: Farmer-specific profile and farm identity data.

**Primary Key**

- `farmer_profile_id uuid pk`

**Foreign Keys**

- `user_id uuid not null fk -> users.user_id`

**Fields**

- `farm_name varchar(150) not null` - Farm or seller name
- `bio text null` - Farmer bio/background
- `farm_location_text text not null` - Main farm location
- `farm_latitude numeric(9,6) null` - Optional map latitude
- `farm_longitude numeric(9,6) null` - Optional map longitude
- `years_of_experience integer null` - Farming experience in years
- `is_public boolean not null default true` - Whether profile is visible in marketplace
- `created_at timestamptz not null default now()`
- `updated_at timestamptz not null default now()`

**Unique Constraints**

- `unique (user_id)`

---

**5.) buyer_profiles**
Purpose: Buyer business profile data.

**Primary Key**

- `buyer_profile_id uuid pk`

**Foreign Keys**

- `user_id uuid not null fk -> users.user_id`

**Fields**

- `business_name varchar(150) not null` - Buyer business name
- `contact_person_name varchar(150) not null` - Main contact person
- `business_type varchar(100) not null` - Restaurant, market, reseller, etc.
- `business_location_text text not null` - Main business address/location
- `business_latitude numeric(9,6) null`
- `business_longitude numeric(9,6) null`
- `created_at timestamptz not null default now()`
- `updated_at timestamptz not null default now()`

**Unique Constraints**

- `unique (user_id)`

---

**6.) distributor_profiles**
Purpose: Distributor operational profile data.

**Primary Key**

- `distributor_profile_id uuid pk`

**Foreign Keys**

- `user_id uuid not null fk -> users.user_id`

**Fields**

- `fleet_display_name varchar(150) not null` - Public distributor/fleet name
- `license_number varchar(100) null` - Professional/operating identifier if needed
- `base_location_text text not null` - Base operating location
- `base_latitude numeric(9,6) null`
- `base_longitude numeric(9,6) null`
- `is_available boolean not null default true` - Distributor availability flag
- `created_at timestamptz not null default now()`
- `updated_at timestamptz not null default now()`

**Unique Constraints**

- `unique (user_id)`
- `unique (license_number)` where license_number is not null

---

**7.) kyc_applications**
Purpose: KYC application submitted per user-role combination.

**Primary Key**

- `kyc_application_id uuid pk`

**Foreign Keys**

- `user_id uuid not null fk -> users.user_id`
- `role_id uuid not null fk -> roles.role_id`

**Fields**

- `application_status text not null` - PENDING, APPROVED, REJECTED, RESUBMISSION_REQUIRED
- `submitted_at timestamptz not null default now()` - Submission time
- `reviewed_at timestamptz null` - Final decision time
- `final_remarks text null` - Final admin remarks
- `created_at timestamptz not null default now()`
- `updated_at timestamptz not null default now()`

**Unique Constraints**

- None at DB level if multiple reapplications are allowed
- If only one active application per user-role is allowed, enforce partial unique index on `(user_id, role_id)` where status in active states

---

**8.) kyc_documents**
Purpose: Metadata of uploaded KYC files tied to an application.

**Primary Key**

- `kyc_document_id uuid pk`

**Foreign Keys**

- `kyc_application_id uuid not null fk -> kyc_applications.kyc_application_id`

**Fields**

- `document_type varchar(100) not null` - Valid ID, business permit, etc.
- `file_url text not null` - S3 or storage URL
- `file_name varchar(255) not null` - Original file name
- `mime_type varchar(100) not null` - File MIME type
- `file_size_bytes bigint null` - File size
- `document_status text not null default 'PENDING'` - PENDING, APPROVED, REJECTED
- `rejection_reason text null` - Reason if rejected
- `uploaded_at timestamptz not null default now()`
- `created_at timestamptz not null default now()`

**Unique Constraints**

- None

---

**9.) kyc_reviews**
Purpose: Admin review history for KYC applications.

**Primary Key**

- `kyc_review_id uuid pk`

**Foreign Keys**

- `kyc_application_id uuid not null fk -> kyc_applications.kyc_application_id`
- `reviewed_by_user_id uuid not null fk -> users.user_id`

**Fields**

- `review_action text not null` - APPROVED, REJECTED, REQUESTED_RESUBMISSION
- `remarks text null` - Admin remarks
- `reviewed_at timestamptz not null default now()`
- `created_at timestamptz not null default now()`

**Unique Constraints**

- None

---

**Marketplace**

**10.) produce_categories**
Purpose: Master list of produce categories.

**Primary Key**

- `produce_category_id uuid pk`

**Fields**

- `category_name varchar(100) not null` - Category name
- `description text null` - Category description
- `created_at timestamptz not null default now()`

**Unique Constraints**

- `unique (category_name)`

---

**11.) produce_listings**
Purpose: Main produce listing created by a farmer.

**Primary Key**

- `produce_listing_id uuid pk`

**Foreign Keys**

- `farmer_profile_id uuid not null fk -> farmer_profiles.farmer_profile_id`
- `produce_category_id uuid not null fk -> produce_categories.produce_category_id`

**Fields**

- `listing_title varchar(150) not null` - Listing title
- `produce_name varchar(150) not null` - Product name
- `description text null` - Listing description
- `price_per_kg numeric(12,2) not null` - Current selling price per kg
- `minimum_order_kg numeric(12,3) not null default 1.000` - Minimum orderable kg
- `maximum_order_kg numeric(12,3) null` - Optional max orderable kg
- `listing_status text not null` - DRAFT, ACTIVE, INACTIVE, SOLD_OUT, ARCHIVED
- `primary_location_text text not null` - Main fulfillment location
- `primary_latitude numeric(9,6) null`
- `primary_longitude numeric(9,6) null`
- `is_premium_boosted boolean not null default false` - Premium ranking flag
- `created_at timestamptz not null default now()`
- `updated_at timestamptz not null default now()`

**Unique Constraints**

- None

---

**12.) produce_listing_images**
Purpose: Images attached to a listing.

**Primary Key**

- `produce_listing_image_id uuid pk`

**Foreign Keys**

- `produce_listing_id uuid not null fk -> produce_listings.produce_listing_id`

**Fields**

- `image_url text not null` - Storage URL
- `display_order integer not null default 1` - Sort order
- `is_primary boolean not null default false` - Primary listing image
- `created_at timestamptz not null default now()`

**Unique Constraints**

- `unique (produce_listing_id, display_order)`

---

**13.) produce_inventory_batches**
Purpose: Harvest/stock records tied to a listing.

**Primary Key**

- `produce_inventory_batch_id uuid pk`

**Foreign Keys**

- `produce_listing_id uuid not null fk -> produce_listings.produce_listing_id`

**Fields**

- `batch_code varchar(100) null` - Internal batch reference
- `estimated_harvest_date date null` - Expected harvest date
- `actual_harvest_date date null` - Actual harvest date
- `available_quantity_kg numeric(12,3) not null` - Available stock in kg
- `reserved_quantity_kg numeric(12,3) not null default 0.000` - Reserved stock in kg
- `inventory_status text not null` - PLANNED, AVAILABLE, RESERVED, DEPLETED
- `notes text null` - Batch notes
- `created_at timestamptz not null default now()`
- `updated_at timestamptz not null default now()`

**Unique Constraints**

- `unique (produce_listing_id, batch_code)` where batch_code is not null

---

**14.) listing_price_history**
Purpose: Historical price change records.

**Primary Key**

- `listing_price_history_id uuid pk`

**Foreign Keys**

- `produce_listing_id uuid not null fk -> produce_listings.produce_listing_id`

**Fields**

- `old_price_per_kg numeric(12,2) not null` - Previous price
- `new_price_per_kg numeric(12,2) not null` - Updated price
- `changed_by_user_id uuid null fk -> users.user_id` - User who changed price
- `effective_at timestamptz not null default now()` - When new price became effective
- `created_at timestamptz not null default now()`

**Unique Constraints**

- None

---

**15.) listing_availability_windows**
Purpose: Specific dates or ranges when listing is actually available.

**Primary Key**

- `listing_availability_window_id uuid pk`

**Foreign Keys**

- `produce_listing_id uuid not null fk -> produce_listings.produce_listing_id`

**Fields**

- `available_from_date date not null` - Start date of availability
- `available_to_date date not null` - End date of availability
- `notes text null` - Optional note on availability
- `created_at timestamptz not null default now()`
- `updated_at timestamptz not null default now()`

**Unique Constraints**

- None

**Notes**

- Add a check: `available_to_date >= available_from_date`

---

**Vehicles**

**16.) vehicle_types**
Purpose: Master list of supported vehicle types and base capacity.

**Primary Key**

- `vehicle_type_id uuid pk`

**Fields**

- `vehicle_type_name varchar(100) not null` - Vehicle name, e.g. Motorcycle, Van, Truck
- `description text null` - Description
- `max_capacity_kg numeric(12,3) not null` - Capacity in kg
- `is_active boolean not null default true`
- `created_at timestamptz not null default now()`

**Unique Constraints**

- `unique (vehicle_type_name)`

---

**17.) farmer_listing_vehicle_types**
Purpose: Allowed delivery vehicle types per listing.

**Primary Key**

- `farmer_listing_vehicle_type_id uuid pk`

**Foreign Keys**

- `produce_listing_id uuid not null fk -> produce_listings.produce_listing_id`
- `vehicle_type_id uuid not null fk -> vehicle_types.vehicle_type_id`

**Fields**

- `created_at timestamptz not null default now()`

**Unique Constraints**

- `unique (produce_listing_id, vehicle_type_id)`

---

**Orders**

**18.) orders**
Purpose: Main buyer transaction record.

**Primary Key**

- `order_id uuid pk`

**Foreign Keys**

- `buyer_user_id uuid not null fk -> users.user_id`

**Fields**

- `order_number varchar(50) not null` - Human-readable order reference
- `order_status text not null` - PENDING_DOWNPAYMENT, CONFIRMED, PREPARING, PICKED_UP, IN_TRANSIT, ARRIVED, COMPLETED, CANCELLED
- `downpayment_due_amount numeric(12,2) not null` - Required downpayment
- `downpayment_paid_amount numeric(12,2) not null default 0.00` - Actual downpayment paid
- `final_payment_due_amount numeric(12,2) not null` - Remaining balance
- `final_payment_paid_amount numeric(12,2) not null default 0.00` - Actual final amount paid
- `subtotal_amount numeric(12,2) not null` - Sum of line items
- `delivery_fee_amount numeric(12,2) not null default 0.00` - Delivery fee
- `platform_fee_amount numeric(12,2) not null default 0.00` - System fee
- `refund_amount numeric(12,2) not null default 0.00` - Refund amount if any
- `delivery_location_text text not null` - Delivery destination
- `delivery_latitude numeric(9,6) null`
- `delivery_longitude numeric(9,6) null`
- `requested_delivery_date date null` - Buyer preferred date
- `downpayment_paid_at timestamptz null` - When order became counted/confirmed
- `completed_at timestamptz null` - Completion time
- `cancelled_at timestamptz null` - Cancellation time
- `created_at timestamptz not null default now()`
- `updated_at timestamptz not null default now()`

**Unique Constraints**

- `unique (order_number)`

---

**19.) order_items**
Purpose: Listing line items inside an order.

**Primary Key**

- `order_item_id uuid pk`

**Foreign Keys**

- `order_id uuid not null fk -> orders.order_id`
- `produce_listing_id uuid not null fk -> produce_listings.produce_listing_id`

**Fields**

- `quantity_kg numeric(12,3) not null` - Ordered weight in kg
- `unit_price_per_kg numeric(12,2) not null` - Snapshotted unit price
- `line_subtotal_amount numeric(12,2) not null` - Line subtotal
- `listing_title_snapshot varchar(150) not null` - Snapshot title for history
- `produce_name_snapshot varchar(150) not null` - Snapshot product name
- `farmer_profile_id uuid not null fk -> farmer_profiles.farmer_profile_id` - Snapshotted listing owner reference
- `created_at timestamptz not null default now()`

**Unique Constraints**

- None

---

**20.) order_status_history**
Purpose: Timeline of order status updates.

**Primary Key**

- `order_status_history_id uuid pk`

**Foreign Keys**

- `order_id uuid not null fk -> orders.order_id`
- `triggered_by_user_id uuid null fk -> users.user_id`

**Fields**

- `from_status text null` - Previous status
- `to_status text not null` - New status
- `remarks text null` - Optional note
- `created_at timestamptz not null default now()`

**Unique Constraints**

- None

---

**21.) order_cancellations**
Purpose: Cancellation record and refund basis.

**Primary Key**

- `order_cancellation_id uuid pk`

**Foreign Keys**

- `order_id uuid not null fk -> orders.order_id`
- `cancelled_by_user_id uuid not null fk -> users.user_id`

**Fields**

- `cancelled_by_role_code varchar(50) not null` - BUYER or FARMER
- `cancellation_reason text not null` - Stated reason
- `cancelled_at timestamptz not null default now()` - Cancellation timestamp
- `refund_policy_applied text not null` - WITHIN_3_BUSINESS_DAYS, AFTER_3_BUSINESS_DAYS, FARMER_FAULT_FULL_REFUND
- `refund_percentage numeric(5,2) not null default 0.00` - Refund rate
- `refund_amount numeric(12,2) not null default 0.00` - Refund amount
- `farmer_kept_amount numeric(12,2) not null default 0.00` - Farmer compensation
- `platform_kept_amount numeric(12,2) not null default 0.00` - Platform retained amount
- `created_at timestamptz not null default now()`

**Unique Constraints**

- `unique (order_id)`

---

**22.) carts**
Purpose: Active buyer cart.

**Primary Key**

- `cart_id uuid pk`

**Foreign Keys**

- `user_id uuid not null fk -> users.user_id`

**Fields**

- `cart_status text not null default 'ACTIVE'` - ACTIVE, CONVERTED, ABANDONED
- `created_at timestamptz not null default now()`
- `updated_at timestamptz not null default now()`

**Unique Constraints**

- `unique (user_id)`

---

**23.) cart_items**
Purpose: Items currently in cart.

**Primary Key**

- `cart_item_id uuid pk`

**Foreign Keys**

- `cart_id uuid not null fk -> carts.cart_id`
- `produce_listing_id uuid not null fk -> produce_listings.produce_listing_id`

**Fields**

- `quantity_kg numeric(12,3) not null` - Desired quantity in kg
- `created_at timestamptz not null default now()`
- `updated_at timestamptz not null default now()`

**Unique Constraints**

- `unique (cart_id, produce_listing_id)`

---

**Payments, Escrow, Wallet**

**24.) wallets**
Purpose: Wallet account per user.

**Primary Key**

- `wallet_id uuid pk`

**Foreign Keys**

- `user_id uuid not null fk -> users.user_id`

**Fields**

- `available_balance numeric(12,2) not null default 0.00` - Spendable balance
- `held_balance numeric(12,2) not null default 0.00` - Escrow/held balance
- `wallet_status text not null default 'ACTIVE'` - ACTIVE, FROZEN, CLOSED
- `created_at timestamptz not null default now()`
- `updated_at timestamptz not null default now()`

**Unique Constraints**

- `unique (user_id)`

---

**25.) wallet_transactions**
Purpose: Wallet ledger entries.

**Primary Key**

- `wallet_transaction_id uuid pk`

**Foreign Keys**

- `wallet_id uuid not null fk -> wallets.wallet_id`
- `payment_id uuid null fk -> payments.payment_id`
- `escrow_transaction_id uuid null fk -> escrow_transactions.escrow_transaction_id`
- `farmer_payout_id uuid null fk -> farmer_payouts.farmer_payout_id`

**Fields**

- `transaction_type text not null` - CREDIT, DEBIT, HOLD, RELEASE, REFUND, PAYOUT
- `amount numeric(12,2) not null` - Ledger amount
- `balance_before numeric(12,2) not null` - Balance before transaction
- `balance_after numeric(12,2) not null` - Balance after transaction
- `reference_code varchar(100) null` - External or internal reference
- `remarks text null` - Explanation
- `created_at timestamptz not null default now()`

**Unique Constraints**

- None

---

**26.) payments**
Purpose: Payment records for downpayment, final payment, refunds.

**Primary Key**

- `payment_id uuid pk`

**Foreign Keys**

- `order_id uuid not null fk -> orders.order_id`

**Fields**

- `payment_type text not null` - DOWNPAYMENT, FINAL_PAYMENT, REFUND
- `payment_method text not null` - GCASH, COD_CASH_TO_DISTRIBUTOR, WALLET
- `payment_status text not null` - PENDING, SUCCESS, FAILED, CANCELLED
- `amount numeric(12,2) not null` - Payment amount
- `external_reference varchar(150) null` - Payment gateway/reference
- `paid_at timestamptz null` - Actual successful payment time
- `created_at timestamptz not null default now()`
- `updated_at timestamptz not null default now()`

**Unique Constraints**

- None

---

**27.) escrow_transactions**
Purpose: Escrow hold, release, forfeiture, refund records.

**Primary Key**

- `escrow_transaction_id uuid pk`

**Foreign Keys**

- `order_id uuid not null fk -> orders.order_id`
- `payment_id uuid null fk -> payments.payment_id`
- `farmer_payout_id uuid null fk -> farmer_payouts.farmer_payout_id`

**Fields**

- `escrow_action text not null` - HOLD, RELEASE_TO_FARMER, REFUND_TO_BUYER, FORFEIT_TO_FARMER, PLATFORM_CUT
- `amount numeric(12,2) not null` - Escrow action amount
- `action_status text not null` - PENDING, COMPLETED, FAILED
- `acted_at timestamptz not null default now()` - When action occurred
- `remarks text null` - Optional explanation
- `created_at timestamptz not null default now()`

**Unique Constraints**

- None

---

**28.) farmer_payouts**
Purpose: Payout released to farmer for an order.

**Primary Key**

- `farmer_payout_id uuid pk`

**Foreign Keys**

- `farmer_profile_id uuid not null fk -> farmer_profiles.farmer_profile_id`
- `order_id uuid not null fk -> orders.order_id`

**Fields**

- `gross_amount numeric(12,2) not null` - Total due before deductions
- `platform_fee_amount numeric(12,2) not null default 0.00` - Platform cut
- `net_amount numeric(12,2) not null` - Final payout amount
- `payout_status text not null` - PENDING, RELEASED, FAILED
- `released_at timestamptz null` - Release time
- `created_at timestamptz not null default now()`
- `updated_at timestamptz not null default now()`

**Unique Constraints**

- `unique (order_id, farmer_profile_id)`

---

**Logistics**

**29.) deliveries**
Purpose: Delivery trip that can carry one or more orders.

**Primary Key**

- `delivery_id uuid pk`

**Foreign Keys**

- `vehicle_type_id uuid not null fk -> vehicle_types.vehicle_type_id`

**Fields**

- `delivery_code varchar(50) not null` - Human-readable delivery reference
- `delivery_status text not null` - PLANNED, ASSIGNED, PICKED_UP, IN_TRANSIT, ARRIVED, COMPLETED, CANCELLED
- `planned_pickup_date timestamptz null` - Planned pickup time
- `actual_pickup_at timestamptz null` - Actual pickup time
- `actual_arrival_at timestamptz null` - Actual arrival time
- `total_reserved_capacity_kg numeric(12,3) not null default 0.000` - Reserved combined load
- `notes text null` - Delivery notes
- `created_at timestamptz not null default now()`
- `updated_at timestamptz not null default now()`

**Unique Constraints**

- `unique (delivery_code)`

---

**30.) delivery_orders**
Purpose: Junction table linking orders to a delivery trip.

**Primary Key**

- `delivery_order_id uuid pk`

**Foreign Keys**

- `delivery_id uuid not null fk -> deliveries.delivery_id`
- `order_id uuid not null fk -> orders.order_id`

**Fields**

- `reserved_capacity_kg numeric(12,3) not null` - Capacity consumed by this order
- `created_at timestamptz not null default now()`

**Unique Constraints**

- `unique (delivery_id, order_id)`
- `unique (order_id)` if each order can belong to only one delivery at a time

---

**31.) delivery_assignments**
Purpose: Distributor assignment history for deliveries.

**Primary Key**

- `delivery_assignment_id uuid pk`

**Foreign Keys**

- `delivery_id uuid not null fk -> deliveries.delivery_id`
- `distributor_user_id uuid not null fk -> users.user_id`

**Fields**

- `assignment_status text not null` - ASSIGNED, ACCEPTED, REPLACED, COMPLETED, CANCELLED
- `assigned_at timestamptz not null default now()` - Assignment time
- `ended_at timestamptz null` - End time if replaced/cancelled
- `created_at timestamptz not null default now()`

**Unique Constraints**

- None
- Usually enforce one active assignment per delivery with a partial unique index

---

**32.) delivery_status_history**
Purpose: Delivery status timeline.

**Primary Key**

- `delivery_status_history_id uuid pk`

**Foreign Keys**

- `delivery_id uuid not null fk -> deliveries.delivery_id`
- `triggered_by_user_id uuid null fk -> users.user_id`

**Fields**

- `from_status text null` - Previous status
- `to_status text not null` - New status
- `remarks text null` - Optional note
- `created_at timestamptz not null default now()`

**Unique Constraints**

- None

---

**Quality Assurance**

**33.) qa_reports**
Purpose: QA report per order within a delivery.

**Primary Key**

- `qa_report_id uuid pk`

**Foreign Keys**

- `delivery_id uuid not null fk -> deliveries.delivery_id`
- `order_id uuid not null fk -> orders.order_id`
- `submitted_by_user_id uuid not null fk -> users.user_id`

**Fields**

- `qa_stage text not null` - AT_PICKUP, AT_DELIVERY
- `fresh_percent numeric(5,2) not null` - Fresh percentage
- `damaged_percent numeric(5,2) not null` - Damaged percentage
- `expected_quantity_kg numeric(12,3) not null` - Expected quantity
- `actual_quantity_kg numeric(12,3) not null` - Actual quantity
- `overall_condition text not null` - EXCELLENT, GOOD, FAIR, POOR, REJECTED
- `notes text null` - QA notes
- `submitted_at timestamptz not null default now()`
- `created_at timestamptz not null default now()`

**Unique Constraints**

- Optional: `unique (delivery_id, order_id, qa_stage)` if only one report per stage per order

**Notes**

- Add a check that `fresh_percent + damaged_percent <= 100.00`

---

**34.) qa_report_images**
Purpose: Photo evidence for QA.

**Primary Key**

- `qa_report_image_id uuid pk`

**Foreign Keys**

- `qa_report_id uuid not null fk -> qa_reports.qa_report_id`

**Fields**

- `image_url text not null` - Storage URL
- `display_order integer not null default 1` - Sort order
- `created_at timestamptz not null default now()`

**Unique Constraints**

- `unique (qa_report_id, display_order)`

---

**35.) qa_issue_flags**
Purpose: Issues flagged during QA.

**Primary Key**

- `qa_issue_flag_id uuid pk`

**Foreign Keys**

- `qa_report_id uuid not null fk -> qa_reports.qa_report_id`

**Fields**

- `issue_type varchar(100) not null` - Damage, spoilage, quantity shortage, etc.
- `severity text not null` - LOW, MEDIUM, HIGH, CRITICAL
- `description text not null` - Issue details
- `created_at timestamptz not null default now()`

**Unique Constraints**

- None

---

**Messaging**

**36.) conversations**
Purpose: One order-specific group chat.

**Primary Key**

- `conversation_id uuid pk`

**Foreign Keys**

- `order_id uuid not null fk -> orders.order_id`

**Fields**

- `conversation_status text not null default 'ACTIVE'` - ACTIVE, CLOSED
- `created_at timestamptz not null default now()`
- `updated_at timestamptz not null default now()`

**Unique Constraints**

- `unique (order_id)`

---

**37.) conversation_participants**
Purpose: Users inside a conversation.

**Primary Key**

- `conversation_participant_id uuid pk`

**Foreign Keys**

- `conversation_id uuid not null fk -> conversations.conversation_id`
- `user_id uuid not null fk -> users.user_id`

**Fields**

- `participant_role_code varchar(50) not null` - FARMER, BUYER, DISTRIBUTOR, ADMIN
- `joined_at timestamptz not null default now()`
- `left_at timestamptz null`
- `created_at timestamptz not null default now()`

**Unique Constraints**

- `unique (conversation_id, user_id)`

---

**38.) messages**
Purpose: Messages sent inside a conversation.

**Primary Key**

- `message_id uuid pk`

**Foreign Keys**

- `conversation_id uuid not null fk -> conversations.conversation_id`
- `sender_user_id uuid not null fk -> users.user_id`

**Fields**

- `message_body text not null` - Message content
- `message_type text not null default 'TEXT'` - TEXT, SYSTEM_NOTICE
- `sent_at timestamptz not null default now()`
- `edited_at timestamptz null`
- `deleted_at timestamptz null`
- `created_at timestamptz not null default now()`

**Unique Constraints**

- None

---

**Reviews**

**39.) reviews**
Purpose: Verified review after successful completed order.

**Primary Key**

- `review_id uuid pk`

**Foreign Keys**

- `order_id uuid not null fk -> orders.order_id`
- `produce_listing_id uuid not null fk -> produce_listings.produce_listing_id`
- `reviewer_user_id uuid not null fk -> users.user_id`

**Fields**

- `star_rating smallint not null` - Required stars, usually 1 to 5
- `review_text text null` - Optional description
- `review_status text not null default 'PUBLISHED'` - PUBLISHED, HIDDEN
- `created_at timestamptz not null default now()`
- `updated_at timestamptz not null default now()`

**Unique Constraints**

- Recommended: `unique (order_id, produce_listing_id, reviewer_user_id)`

**Notes**

- Add a check: `star_rating between 1 and 5`

---

**Logs**

**40.) audit_logs**
Purpose: Traceability for sensitive actions.

**Primary Key**

- `audit_log_id uuid pk`

**Foreign Keys**

- `actor_user_id uuid null fk -> users.user_id`

**Fields**

- `entity_type varchar(100) not null` - Table/domain acted on
- `entity_id uuid null` - Record identifier acted on
- `action_type varchar(100) not null` - CREATE, UPDATE, DELETE, APPROVE, REJECT, etc.
- `action_summary text not null` - Short explanation of what happened
- `metadata jsonb null` - Extra structured context
- `ip_address inet null` - Origin IP if relevant
- `user_agent text null` - Caller/client info
- `created_at timestamptz not null default now()`

**Unique Constraints**

- None
