# dump build stage
FROM postgres:12 as dumper

COPY dbscripts/init.sql /docker-entrypoint-initdb.d/
COPY dbscripts/populate.sql /docker-entrypoint-initdb.d/

RUN ["sed", "-i", "s/exec \"$@\"/echo \"skipping...\"/", "/usr/local/bin/docker-entrypoint.sh"]

RUN ["/usr/local/bin/docker-entrypoint.sh", "postgres"]

# final build stage
FROM postgres:12

EXPOSE 5432
COPY --from=dumper /data $PGDATA


