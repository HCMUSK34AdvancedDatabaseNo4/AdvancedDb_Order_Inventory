#!/bin/bash
set -e

# Create data tables
psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" -f /docker-entrypoint-initdb.d/init.sql

# Import data into created data tables
psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
    COPY product_inventory(id, updated_date, stock) 
    FROM '/var/lib/postgresql/productData.csv' 
    DELIMITER ';' 
    CSV HEADER;
EOSQL

echo "Tables created and data import completed"