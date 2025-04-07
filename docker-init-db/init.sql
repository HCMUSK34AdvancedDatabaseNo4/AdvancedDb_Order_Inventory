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

CREATE TABLE product_inventory (
    id TEXT PRIMARY KEY,
    updated_date TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
    stock INTEGER NOT NULL  
);

CREATE TABLE order_item (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    product_id TEXT NOT NULL,
    quantity INTEGER NOT NULL,  
    price NUMERIC NOT NULL,
    subtotal NUMERIC NOT NULL,
    order_id UUID,

    CONSTRAINT fk_order_item_order_id
    FOREIGN KEY (order_id)
    REFERENCES "order"(id)
    ON DELETE CASCADE,

    CONSTRAINT fk_order_item_product_inventory
    FOREIGN KEY (product_id)
    REFERENCES product_inventory(id)
);

-- Enhances join and filtering performance when accessing order items for a particular order.
CREATE INDEX idx_order_item_order_id
    ON order_item (order_id);

-- Optimizes queries that fetch orders for a specific user
CREATE INDEX idx_order_user_id
    ON "order" (user_id);

-- Improves query performance for date-based filtering and sorting orders
CREATE INDEX idx_order_created_date
    ON "order" (created_date);
