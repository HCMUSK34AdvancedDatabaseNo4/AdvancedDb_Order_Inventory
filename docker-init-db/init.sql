CREATE EXTENSION IF NOT EXISTS pgcrypto;

CREATE TABLE "order" (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id TEXT NOT NULL,  
    voucher_id INTEGER,         
    total_price NUMERIC NOT NULL,
    shipment_status TEXT,
    payment_status TEXT,
    updated_date TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    created_date TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    payment_method TEXT
);

CREATE TABLE order_item (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    product_id TEXT NOT NULL,
    quantity INTEGER NOT NULL,  
    price NUMERIC NOT NULL,
    subtotal NUMERIC NOT NULL,
    order_id UUID
);

CREATE TABLE product_inventory (
    id TEXT PRIMARY KEY,
    updated_date TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    stock INTEGER NOT NULL  
);