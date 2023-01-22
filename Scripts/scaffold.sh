#!/usr/bin/env sh

cd Models || exit
dotnet ef dbcontext scaffold \
  "Server=127.0.0.1,1433;Database=Master;User Id=SA;Password=yourStrong(!)Password" \
  Microsoft.EntityFrameworkCore.SqlServer \
  --project Models.csproj --context CMSDbContext \
  --force
cd ..
