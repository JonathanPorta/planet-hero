#! /bin/bash

checkProcess() {
  if kill -0 "$1" 2>/dev/null; then
    echo "$1 is still alive."
  elif wait "$1"; then
    echo "$1 exited with zero status."
    exit 0
  else
    echo "$1 exited with non-zero status."
    exit 1
  fi
}

echo "Forking build process..."
./Scripts/build.sh &
build_pid=$!

echo "Build forked with PID $build_pid"

touch unity.log
tail -f ./unity.log &

while true
do
  checkProcess $build_pid
  sleep 10
done
