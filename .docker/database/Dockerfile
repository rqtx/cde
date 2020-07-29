# dump build stage
FROM postgres:12 as dumper

ENV POSTGRES_PASSWORD=ov3rl0rd
ENV POSTGRES_DB=cde
ENV POSTGRES_USER=postgres
ENV PGDATA=/data
ENV TZ=GMT

COPY dbscripts/init.sql /docker-entrypoint-initdb.d/
COPY dbscripts/populate.sql /docker-entrypoint-initdb.d/

RUN ["sed", "-i", "s/exec \"$@\"/echo \"skipping...\"/", "/usr/local/bin/docker-entrypoint.sh"]

RUN ["/usr/local/bin/docker-entrypoint.sh", "postgres"]

# final build stage
FROM postgres:12

EXPOSE 5432
COPY --from=dumper /data $PGDATA


