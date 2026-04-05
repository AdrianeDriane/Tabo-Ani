# Tabo-Ani Digital - Agricultural Supply Chain Platform

A comprehensive digital platform connecting farmers, distributors, buyers, and retailers to promote efficiency, transparency, and sustainability in the Philippine agricultural supply chain.

## Getting Started

1. Run `npm install`
2. Run `npm run dev`

## Auth and RBAC

Implementation and reuse notes for login/session handling, RBAC controller protection, frontend route guards, and the temporary auth QA routes live in:

- `docs/auth-login-and-rbac.md`

## Platform Pages & Features

### 1. Landing Page (`/`)

Landing Page presents the public-facing entry point of the Tabo-Ani Digital platform. This page showcases the platform's core mission of connecting farmers, distributors, and buyers to improve agricultural market access and distribution efficiency. The page features a prominent navigation bar with options for Home, About Us, Listings, Deliveries, and Contacts, along with Sign Up and Log In buttons. The layout highlights four user type cards explaining the platform's value proposition for Farmers, Distributors, Buyers, and Retailers. A Premium Features section emphasizes the platform's innovation in order tracking, escrow payments, QA reporting, and real-time messaging. The design uses a fresh green and white color scheme reflecting agricultural growth and sustainability, creating an inviting and professional interface.

### 2. Signup Page (`/signup`)

Signup Page presents the user registration interface supporting multi-role onboarding for farmers, distributors, buyers, and retailers. The page implements a comprehensive 4-step registration flow: (1) Account Credentials capturing email/phone number and password, (2) Account Setup with user type selection and business details, (3) KYC Verification with government ID document uploads and verification status, and (4) Confirmation of registered details. The page integrates Google OAuth for quick account creation and includes mobile number validation with OTP. Each step includes clear progress indicators and validation feedback, with role-specific fields adapted to user types. The clean form-based design with consistent Tabo-Ani branding ensures a smooth onboarding experience while maintaining compliance with Know-Your-Customer (KYC) requirements.

### 3. Login Page (`/login`)

Login Page presents the authentication interface with role-based routing for different user dashboards. The page supports dual login methods: email or mobile number entry with corresponding validation. Google OAuth integration provides quick authentication for returning users. Upon successful login, the system automatically routes users to their respective role-based dashboards (Farmer Dashboard, Buyer Dashboard, Distributor Dashboard, or Admin Dashboard) ensuring personalized experiences. The page includes account recovery options and clear messaging about account security, with responsive design optimized for both desktop and mobile access.

### 4. Terms and Conditions (`/terms`)

Terms and Conditions Page presents the platform's legal framework governing user conduct, transaction policies, and dispute resolution procedures. The page covers comprehensive sections including user responsibilities, payment and escrow policies, quality assurance requirements, farmer protection measures, distributor obligations, buyer guarantees, refund and cancellation policies, and platform liability limitations. The document establishes clear expectations for all stakeholders, promotes fair and transparent transactions, and provides dispute resolution mechanisms. Users must agree to these terms during signup to ensure compliance with platform standards and legal requirements.

### 5. Farmer Dashboard (`/dashboard`)

Farmer Dashboard presents the primary workspace for agricultural producers managing their operations on the Tabo-Ani platform. The dashboard displays comprehensive metrics including total sales, orders pending, gross revenue, and wallet balance. The page features an active orders section highlighting pending shipments with status badges and buyer information. An earnings breakdown chart visualizes revenue streams through marketplace sales and direct orders. Quick-access links to Wallet and Analytics pages enable financial management. The layout includes links to create new product listings and access the AI Chatbot for business guidance. The design emphasizes operational efficiency and financial transparency, helping farmers monitor sales performance and manage customer relationships effectively.

### 6. Marketplace (`/marketplace`)

Marketplace Page presents the central catalog of agricultural products available from verified farmers across the platform. The page includes a search and filter interface allowing buyers to discover produce by category (Vegetables, Fruits, Grains), price range, and seller location. Each product card displays high-resolution images, product name, unit price (₱), available quantity, farmer name with star rating, delivery timeframe, and quick-add-to-cart functionality. Featured products section highlights popular seasonal items. The responsive grid layout adapts to desktop, tablet, and mobile viewports ensuring accessibility for all users. The marketplace emphasizes product quality, farmer credibility, and frictionless discovery driving buyer engagement and farmer sales.

### 7. Product Detail (`/product/:id`)

Product Detail Page presents comprehensive information about a specific agricultural product selected from the marketplace. The page features a high-quality product image gallery, essential details including product name, price, available quantity, unit type, and farmer seller information with ratings. An Availability & Shipping section shows stock status, delivery timeframes, and service areas. The Farmer Profile section links to the seller's full profile enabling buyer-farmer relationship building. A detailed Product Description covers harvesting practices, organic certification status, and handling recommendations. Customer Reviews & Ratings display verified buyer feedback with star ratings and review text. The Add to Cart button with quantity selection enables seamless purchasing, while the farmer's contact information facilitates direct inquiries enhancing buyer confidence and purchase decisions.

### 8. Farmer Profile (`/farmer/:id`)

Farmer Profile Page presents a comprehensive profile of individual agricultural producers on the platform. The page displays farmer name, location, profile image, verified status badge, and overall ratings calculated from all customer reviews. A farm gallery showcases high-quality images of the farm, crops, and operations building trust and transparency. Farm details section covers farm size, farming methods (conventional/organic), years of experience, specializations, and certifications. A buyer review cards section highlights verified customer feedback with star ratings, review text, and review dates from past transactions. The profile includes links to the farmer's marketplace listings and ability to initiate direct contact through messaging. The design emphasizes farmer credibility, product quality standards, and buyer confidence in agricultural sourcing.

### 9. Messages (`/messages`)

Messages Page presents the order-specific group chat interface connecting Farmers, Distributors, and Buyers for each transaction. The page organizes conversations in a sidebar list showing order IDs, product names, participant role badges (farmer green, distributor blue, buyer white), unread message counts, and last message previews. The main chat area displays message threads with role-colored participant pills, message timestamps, and safety notices reminding users to keep transactions within the platform. A mobile-responsive design includes a back button to switch between conversation list and chat view on smaller screens. The input bar at the bottom with text field and send button remains consistently visible enabling seamless communication. The interface promotes transparent communication and dispute resolution while safeguarding user privacy and transaction security.

### 10. Order Tracking (`/orders`)

Order Tracking Page presents detailed status monitoring for all user transactions throughout the fulfillment lifecycle. The page displays orders in a tabbed interface (All/Active/Completed) with status badges indicating current stage (Pending/In Transit/Delivered/Cancelled). Each order card shows order ID, product names and quantities, order total including fees, and the complete 5-step timeline: Order Placed → Payment Received → Ready for Pickup → In Transit → Delivered. A visual progress bar indicates the current step. An Escrow Payment Status section displays security information about funds held during fulfillment. A Freshness Assurance Bar shows the QA freshness percentage from distributor inspection. Detailed delivery information includes pickup location, delivery location, estimated delivery date, and distributor contact. The interface enables users to track shipments in real-time, verify product quality standards, and initiate dispute resolution if needed.

### 11. Admin Dashboard (`/admin`)

Admin Dashboard presents the administrative control center for platform management and oversight. The dashboard displays key performance indicators including total users by role (farmers, distributors, buyers), total transactions volume, transaction value in ₱, and platform health metrics. The page includes user management interfaces for viewing, verifying, and managing user accounts across all roles. A transactions overview section shows recent orders, payment status, dispute resolutions, and revenue analytics. QA report monitoring enables oversight of product quality standards and distributor compliance. An analytics section provides insights into platform usage patterns, popular product categories, regional distribution, and financial performance trends. Admin-specific controls allow configuration of platform settings, fee structures, and dispute resolution processes. The interface emphasizes operational transparency and platform reliability.

### 12. Checkout Page (`/checkout`)

Checkout Page presents the secure payment interface for completing agricultural product purchases. The page displays order summary with itemized product list including quantities, unit prices, subtotal, delivery fee, and total amount in ₱. Delivery address section includes form fields for full name, street address, city/municipality, province, and postal code with validation. The payment method selection interface supports multiple options including bank transfer, credit/debit card, GCash mobile wallet, and other local payment processors. The Order Notes field enables special handling instructions for agricultural shipments. A review order button triggers a confirmation step. Upon successful payment, the system initiates order fulfillment and provides order confirmation with order ID, estimated delivery date, and tracking instructions. The design prioritizes security, payment accuracy, and user confidence in agricultural purchasing.

### 13. QA Reporting Page (`/qa-reporting`)

QA Reporting Page presents the quality assurance interface for Tabo-Ani Fleet distributors verifying produce condition at pickup and delivery stages. The page displays assigned delivery list in a sidebar showing order IDs, product names, quantities, and current status badges. The main form section captures quality metrics including delivery information (farmer to buyer), QA stage selection (At Pickup/At Delivery), and Freshness Assessment with dual sliders for fresh percentage and damaged percentage with real-time visual bar. Quantity verification fields capture expected versus actual received amounts. Overall Condition dropdown selects from Excellent to Rejected status. The Photo Evidence section requires minimum 2 photos for documentation. Additional Notes textarea captures inspection observations. Flag Issue and Submit QA Report buttons enable report submission and issue escalation. The submitted state displays confirmation with freshness metrics and notification to stakeholders. The interface ensures transparent quality standards and accountability in agricultural distribution.

### 14. Analytics Page (`/analytics`)

Analytics Page presents comprehensive business intelligence dashboards for farmers and other stakeholders monitoring platform performance. The page displays sales trends through interactive charts showing revenue over time (daily/weekly/monthly), sales by product category, and geographic performance distribution. Key metrics cards show total sales volume, average order value, customer retention rate, and growth trends. A customer insights section provides buyer demographics, repeat purchase rates, and customer satisfaction scores. Product performance analysis ranks products by sales volume, revenue contribution, and customer ratings. Traffic source analytics show how customers discover farm products (marketplace search, direct, referral, etc.). Regional sales breakdown displays performance across Philippine provinces. The interface enables data-driven decision-making, inventory management optimization, and pricing strategy adjustments based on market demand and customer preferences.

### 15. Buyer Dashboard (`/buyer-dashboard`)

Buyer Dashboard presents the primary workspace for retail businesses and restaurants purchasing agricultural products through Tabo-Ani. The dashboard displays purchasing metrics including total spend, active orders, saved suppliers, and rebate balance. A supplier directory section shows frequently used farmers and distributors with quick reorder functionality. Recent orders section lists recent purchases with status, total spent, and Quick Reorder buttons. Search and discovery features help locate specific products or suppliers. An order history timeline shows past transactions with invoice access. A favorites/preferred suppliers list enables quick purchasing from trusted partners. Wallet and payment management links facilitate financial operations. The layout emphasizes supplier relationship management, purchase efficiency, and cost optimization enabling buyers to streamline procurement of high-quality agricultural products.

### 16. Wallet Page (`/wallet`)

Wallet Page presents the financial management interface for all user roles managing funds, payments, and earnings on the Tabo-Ani platform. The page displays account balance in ₱ with funding methods for adding money (bank transfer, debit card, e-wallet options). A transaction history lists all deposits, purchases, refunds, earnings, and transfers with dates and amounts. Income breakdown section shows earnings by transaction type (sales, delivery fees, bonuses). Pending transactions section displays payments in escrow awaiting delivery confirmation. Withdrawal options enable users to transfer balance to registered bank accounts. Transaction filtering enables searching by date, amount, or transaction type. A payment methods management section allows adding/removing bank accounts, credit cards, and e-wallets. The interface emphasizes financial security, transparency, and accessibility ensuring users maintain control over their funds and understand their financial flow through the platform.

### 17. Distributor Dashboard (`/distributor-dashboard`)

Distributor Dashboard presents the fleet management interface for logistics partners operating the Tabo-Ani delivery network. The dashboard displays operational metrics including active deliveries count, completed deliveries today, total deliveries lifetime, and delivery earnings in ₱. A delivery list with tabbed filtering (All/Active/Completed) shows assigned orders with status badges (Assigned/Picked Up/In Transit/Delivered). Each delivery card displays product name, quantity, order ID, scheduled date, and pickup→delivery route visualization with green pickup and red delivery location dots. The route section shows farmer pickup location, buyer delivery location, and estimated time. Earnings display shows payment in ₱ and delivery fee. Action buttons include Start Pickup, Submit QA Report, Mark In Transit, Group Chat, and Report Issue options. Stats cards show active delivery counts, completion rates, and performance metrics. The interface enables efficient fleet coordination, real-time tracking, and order fulfillment optimization.

### 18. AI Chatbot Component

AI Chatbot presents an intelligent conversational assistant helping users navigate platform features, answer agricultural and marketplace questions, and provide business guidance. The chatbot appears as a floating button on the platform when users are logged in. Clicking opens a collapsible chat interface displaying conversation history and input field. The chatbot supports queries about product listings, pricing, delivery timelines, farmer profiles, order status, payment methods, dispute resolution, and general platform guidance. AI responses are contextually aware utilizing platform data and user role (farmer, distributor, buyer, retailer). The chatbot learns from interactions improving response accuracy over time. Commands include clear chat history, view FAQs, and escalate to human support. The conversational design ensures accessibility for users with varying technical literacy while reducing support burden and improving user satisfaction through instant, available assistance.

