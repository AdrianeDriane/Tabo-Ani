## Access

### 1.) users

- users to user_roles = 1:N
  - One user can have many role assignments
  - Connector label: user has many user roles

- users to farmer_profiles = 1:1
  - One user can have one farmer profile
  - Connector label: user has one farmer profile

- users to buyer_profiles = 1:1
  - One user can have one buyer profile
  - Connector label: user has one buyer profile

- users to distributor_profiles = 1:1
  - One user can have one distributor profile
  - Connector label: user has one distributor profile

- users to kyc_applications = 1:N
  - One user can submit many KYC applications
  - Connector label: user submits many kyc applications

- users to carts = 1:1
  - One buyer user has one active cart
  - Connector label: user has one cart

- users to orders = 1:N
  - One buyer user can place many orders
  - Connector label: user places many orders

- users to wallets = 1:1
  - One user has one wallet
  - Connector label: user has one wallet

- users to delivery_assignments = 1:N
  - One distributor user can receive many delivery assignments
  - Connector label: user receives many delivery assignments

- users to conversation_participants = 1:N
  - One user can participate in many conversations
  - Connector label: user joins many conversation participants

- users to messages = 1:N
  - One user can send many messages
  - Connector label: user sends many messages

- users to reviews = 1:N
  - One user can create many reviews
  - Connector label: user writes many reviews

- users to audit_logs = 1:N
  - One user can trigger many audit log entries
  - Connector label: user triggers many audit logs

---

### 2.) roles

- roles to user_roles = 1:N
  - One role can be assigned to many users
  - Connector label: role is assigned to many user roles

---

### 3.) user_roles

- user_roles bridges users and roles = M:N resolved by junction table
  - Connector label from junction perspective:
    - user role belongs to one user
    - user role belongs to one role

---

## User Profiles and Verification

### 4.) farmer_profiles

- farmer_profiles to users = 1:1
  - Connector label: farmer profile belongs to one user

- farmer_profiles to produce_listings = 1:N
  - One farmer profile can own many produce listings
  - Connector label: farmer profile has many produce listings

- farmer_profiles to farmer_payouts = 1:N
  - One farmer profile can receive many payouts
  - Connector label: farmer profile receives many farmer payouts

---

### 5.) buyer_profiles

- buyer_profiles to users = 1:1
  - Connector label: buyer profile belongs to one user

---

### 6.) distributor_profiles

- distributor_profiles to users = 1:1
  - Connector label: distributor profile belongs to one user

---

### 7.) kyc_applications

- kyc_applications to users = 1:N
  - One user can submit many KYC applications
  - Connector label: kyc application belongs to one user

- kyc_applications to roles = 1:N
  - One role can be the target of many KYC applications
  - Connector label: kyc application targets one role

- kyc_applications to kyc_documents = 1:N
  - One KYC application can have many documents
  - Connector label: kyc application has many kyc documents

- kyc_applications to kyc_reviews = 1:N
  - One KYC application can have many review records
  - Connector label: kyc application has many kyc reviews

---

### 8.) kyc_documents

- kyc_documents to kyc_applications = N:1
  - Connector label: kyc document belongs to one kyc application

---

### 9.) kyc_reviews

- kyc_reviews to kyc_applications = N:1
  - Connector label: kyc review belongs to one kyc application

- kyc_reviews to users = N:1
  - Admin user performs the review
  - Connector label: kyc review is made by one user

---

## Marketplace

### 10.) produce_categories

- produce_categories to produce_listings = 1:N
  - One category can contain many produce listings
  - Connector label: produce category has many produce listings

---

### 11.) produce_listings

- produce_listings to farmer_profiles = N:1
  - Connector label: produce listing belongs to one farmer profile

- produce_listings to produce_categories = N:1
  - Connector label: produce listing belongs to one produce category

- produce_listings to produce_listing_images = 1:N
  - One listing can have many images
  - Connector label: produce listing has many produce listing images

- produce_listings to produce_inventory_batches = 1:N
  - One listing can have many inventory batches
  - Connector label: produce listing has many produce inventory batches

- produce_listings to listing_price_history = 1:N
  - One listing can have many price history records
  - Connector label: produce listing has many listing price history records

- produce_listings to listing_availability_windows = 1:N
  - One listing can have many availability windows
  - Connector label: produce listing has many listing availability windows

- produce_listings to order_items = 1:N
  - One listing can appear in many order items
  - Connector label: produce listing appears in many order items

- produce_listings to farmer_listing_vehicle_types = 1:N
  - One listing can allow many vehicle types
  - Connector label: produce listing allows many farmer listing vehicle types

- produce_listings to reviews = 1:N
  - One listing can receive many reviews
  - Connector label: produce listing has many reviews

---

### 12.) produce_listing_images

- produce_listing_images to produce_listings = N:1
  - Connector label: produce listing image belongs to one produce listing

---

### 13.) produce_inventory_batches

- produce_inventory_batches to produce_listings = N:1
  - Connector label: produce inventory batch belongs to one produce listing

---

### 14.) listing_price_history

- listing_price_history to produce_listings = N:1
  - Connector label: listing price history belongs to one produce listing

---

### 15.) listing_availability_windows

- listing_availability_windows to produce_listings = N:1
  - Connector label: listing availability window belongs to one produce listing

---

## Vehicles

### 16.) vehicle_types

- vehicle_types to farmer_listing_vehicle_types = 1:N
  - One vehicle type can be allowed by many listings
  - Connector label: vehicle type is used by many farmer listing vehicle types

- vehicle_types to deliveries = 1:N
  - One vehicle type can be used in many deliveries
  - Connector label: vehicle type is used by many deliveries

---

### 17.) farmer_listing_vehicle_types

- Bridges produce_listings and vehicle_types = M:N resolved by junction table
  - Connector labels:
    - farmer listing vehicle type belongs to one produce listing
    - farmer listing vehicle type belongs to one vehicle type

---

## Orders

### 18.) orders

- orders to users = N:1
  - Buyer places the order
  - Connector label: order belongs to one user

- orders to order_items = 1:N
  - One order has many line items
  - Connector label: order has many order items

- orders to order_status_history = 1:N
  - One order has many status records
  - Connector label: order has many order status history records

- orders to order_cancellations = 1:0..1
  - One order may have one cancellation record
  - In ERD terms, usually model as 1:1 optional
  - Connector label: order may have one order cancellation

- orders to payments = 1:N
  - One order can have many payments
  - Connector label: order has many payments

- orders to escrow_transactions = 1:N
  - One order can have many escrow records
  - Connector label: order has many escrow transactions

- orders to delivery_orders = 1:N
  - One order can appear in delivery linkage records
  - Connector label: order has many delivery orders

- orders to conversations = 1:1
  - One order has one group conversation
  - Connector label: order has one conversation

- orders to reviews = 1:N
  - One order can have many reviews only if needed per listing item
  - Connector label: order has many reviews

---

### 19.) order_items

- order_items to orders = N:1
  - Connector label: order item belongs to one order

- order_items to produce_listings = N:1
  - Connector label: order item belongs to one produce listing

---

### 20.) order_status_history

- order_status_history to orders = N:1
  - Connector label: order status history belongs to one order

- order_status_history to users = N:1 optional
  - If you want who triggered the status
  - Connector label: order status history is triggered by one user

---

### 21.) order_cancellations

- order_cancellations to orders = 1:1
  - Connector label: order cancellation belongs to one order

- order_cancellations to users = N:1
  - The actor who cancelled
  - Connector label: order cancellation is made by one user

---

### 22.) carts

- carts to users = 1:1
  - One buyer user has one active cart
  - Connector label: cart belongs to one user

- carts to cart_items = 1:N
  - One cart has many items
  - Connector label: cart has many cart items

---

### 23.) cart_items

- cart_items to carts = N:1
  - Connector label: cart item belongs to one cart

- cart_items to produce_listings = N:1
  - Connector label: cart item belongs to one produce listing

---

## Payments, Escrow, Wallet

### 24.) wallets

- wallets to users = 1:1
  - Connector label: wallet belongs to one user

- wallets to wallet_transactions = 1:N
  - Connector label: wallet has many wallet transactions

---

### 25.) wallet_transactions

- wallet_transactions to wallets = N:1
  - Connector label: wallet transaction belongs to one wallet

- wallet_transactions to payments = N:1 optional
  - If wallet movement is tied to a payment event
  - Connector label: wallet transaction may belong to one payment

- wallet_transactions to escrow_transactions = N:1 optional
  - If wallet movement is tied to escrow action
  - Connector label: wallet transaction may belong to one escrow transaction

- wallet_transactions to farmer_payouts = N:1 optional
  - If tied to payout release
  - Connector label: wallet transaction may belong to one farmer payout

---

### 26.) payments

- payments to orders = N:1
  - Connector label: payment belongs to one order

- payments to wallet_transactions = 1:N optional inverse
  - A payment may generate wallet entries

- payments to escrow_transactions = 1:N
  - One payment can create one or more escrow actions
  - Connector label: payment has many escrow transactions

---

### 27.) escrow_transactions

- escrow_transactions to orders = N:1
  - Connector label: escrow transaction belongs to one order

- escrow_transactions to payments = N:1 optional
  - Connector label: escrow transaction may belong to one payment

- escrow_transactions to wallet_transactions = 1:N optional inverse
  - One escrow action may create wallet ledger effects

- escrow_transactions to farmer_payouts = N:1 optional
  - Final release can be tied to payout
  - Connector label: escrow transaction may belong to one farmer payout

---

### 28.) farmer_payouts

- farmer_payouts to farmer_profiles = N:1
  - Connector label: farmer payout belongs to one farmer profile

- farmer_payouts to orders = N:1
  - One farmer payout is tied to one order
  - Connector label: farmer payout belongs to one order

---

## Logistics

### 29.) deliveries

- deliveries to vehicle_types = N:1
  - One delivery uses one vehicle type
  - Connector label: delivery uses one vehicle type

- deliveries to delivery_orders = 1:N
  - One delivery can carry many orders
  - Connector label: delivery has many delivery orders

- deliveries to delivery_assignments = 1:N
  - One delivery can have assignment history, but usually one active assignment
  - Connector label: delivery has many delivery assignments

- deliveries to delivery_status_history = 1:N
  - One delivery has many status records
  - Connector label: delivery has many delivery status history records

- deliveries to qa_reports = 1:N
  - One delivery can have many QA reports
  - Connector label: delivery has many qa reports

---

### 30.) delivery_orders

- Bridges deliveries and orders = M:N resolved by junction table
  - Connector labels:
    - delivery order belongs to one delivery
    - delivery order belongs to one order

---

### 31.) delivery_assignments

- delivery_assignments to deliveries = N:1
  - Connector label: delivery assignment belongs to one delivery

- delivery_assignments to users = N:1
  - Assigned distributor user
  - Connector label: delivery assignment belongs to one user

---

### 32.) delivery_status_history

- delivery_status_history to deliveries = N:1
  - Connector label: delivery status history belongs to one delivery

- delivery_status_history to users = N:1 optional
  - If you track who updated status
  - Connector label: delivery status history is triggered by one user

---

## Quality Assurance

### 33.) qa_reports

- qa_reports to deliveries = N:1
  - Connector label: qa report belongs to one delivery

- qa_reports to orders = N:1 optional but recommended
  - Because one delivery can contain multiple orders, QA should be identifiable per order
  - Connector label: qa report belongs to one order

- qa_reports to users = N:1
  - Distributor submits QA
  - Connector label: qa report is submitted by one user

- qa_reports to qa_report_images = 1:N
  - Connector label: qa report has many qa report images

- qa_reports to qa_issue_flags = 1:N
  - Connector label: qa report has many qa issue flags

---

### 34.) qa_report_images

- qa_report_images to qa_reports = N:1
  - Connector label: qa report image belongs to one qa report

---

### 35.) qa_issue_flags

- qa_issue_flags to qa_reports = N:1
  - Connector label: qa issue flag belongs to one qa report

---

## Messaging

### 36.) conversations

- conversations to orders = 1:1
  - Connector label: conversation belongs to one order

- conversations to conversation_participants = 1:N
  - Connector label: conversation has many conversation participants

- conversations to messages = 1:N
  - Connector label: conversation has many messages

---

### 37.) conversation_participants

- Bridges conversations and users = M:N resolved by junction table
  - Connector labels:
    - conversation participant belongs to one conversation
    - conversation participant belongs to one user

---

### 38.) messages

- messages to conversations = N:1
  - Connector label: message belongs to one conversation

- messages to users = N:1
  - Connector label: message is sent by one user

---

## Reviews

### 39.) reviews

- reviews to orders = N:1
  - Review must come from a successful order
  - Connector label: review belongs to one order

- reviews to produce_listings = N:1
  - Review is on a produce listing
  - Connector label: review belongs to one produce listing

- reviews to users = N:1
  - Reviewer
  - Connector label: review is written by one user

---

## Logs

### 40.) audit_logs

- audit_logs to users = N:1 optional
  - Connector label: audit log is triggered by one user
