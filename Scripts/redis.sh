docker run \
  --name course-management-system-cache \
  -p 6379:6379 \
  -v course-management-system-cache-backup:/data \
  redis:7.0.7 \
  --save 60 1 \
  --loglevel warning
