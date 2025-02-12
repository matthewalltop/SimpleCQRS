## Setup
---

1. Run the compose file - `docker compose up -d`

2. tty container & create the `martendb` user

```bash

> psql
postgres-# CREATE USER martendb WITH PASSWORD <some-password> SUPERUSER;
postgres-# CREATE DATABASE marten_test WITH OWNER martendb;

# List users
postgres-# \du

# List databases
postgres-# \l

# List Tables
postgres-# \dt

```