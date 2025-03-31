#!/bin/bash
set -e

# Для применения миграций
dotnet ef database update

# Собственно запуск приложения
exec dotnet API.dll