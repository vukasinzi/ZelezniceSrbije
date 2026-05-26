#!/usr/bin/env bash
set -e

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "$SCRIPT_DIR/../.." && pwd)"

cd "$ROOT_DIR"
source .env

DB_NAME="ZelezniceSrbije"
CONTAINER_ID=$(docker compose ps -q db)

echo "Čekam SQL Server..."

until docker compose exec -T db /opt/mssql-tools18/bin/sqlcmd \
  -S localhost \
  -U sa \
  -P "$SA_PASSWORD" \
  -C \
  -Q "SELECT 1" > /dev/null 2>&1
do
  sleep 2
done

echo "Kreiram bazu ako ne postoji..."

docker compose exec -T db /opt/mssql-tools18/bin/sqlcmd \
  -S localhost \
  -U sa \
  -P "$SA_PASSWORD" \
  -C \
  -Q "IF DB_ID(N'$DB_NAME') IS NULL CREATE DATABASE [$DB_NAME];"

echo "Kopiram SQL fajlove..."

docker cp "$SCRIPT_DIR/001_schema.sql" "$CONTAINER_ID:/tmp/001_schema.sql"
docker cp "$SCRIPT_DIR/seed.sql" "$CONTAINER_ID:/tmp/seed.sql"

echo "Izvršavam schemu..."

docker compose exec -T db /opt/mssql-tools18/bin/sqlcmd \
  -S localhost \
  -U sa \
  -P "$SA_PASSWORD" \
  -C \
  -d "$DB_NAME" \
  -i /tmp/001_schema.sql

echo "Izvršavam seed..."

docker compose exec -T db /opt/mssql-tools18/bin/sqlcmd \
  -S localhost \
  -U sa \
  -P "$SA_PASSWORD" \
  -C \
  -d "$DB_NAME" \
  -i /tmp/seed.sql

echo "Baza je spremna."