set -e 

docker compose down || true
git pull origin main

docker compose up --build