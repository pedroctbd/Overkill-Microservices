#!/bin/sh

# Wait for Kafka Connect REST API to be ready
echo "Waiting for Kafka Connect to be available..."
until curl -s http://connect:8083/; do
  echo "Kafka Connect not available yet, retrying in 5 seconds..."
  sleep 5
done

echo "Kafka Connect is up! Registering Debezium Postgres connector..."

curl -X POST -H "Content-Type: application/json" --fail -d '{
  "name": "postgres-connector",
  "config": {
    "connector.class": "io.debezium.connector.postgresql.PostgresConnector",
    "database.hostname": "your_postgres_host",
    "database.port": "5432",
    "database.user": "postgres",
    "database.password": "admin",
    "database.dbname": "microservicesOK",
    "database.server.name": "dbserver1",
    "table.include.list": "public.EventEnvelopes",
    "plugin.name": "pgoutput",
    "slot.name": "debezium_slot",
    "publication.name": "debezium_pub"
  }
}' http://connect:8083/connectors || echo "Connector already registered or failed."

# Keep container running to not exit
tail -f /dev/null
