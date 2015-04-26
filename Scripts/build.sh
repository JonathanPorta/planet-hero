#! /bin/sh

# This script is meant to wrap the Unity3D build process and ensure that output
# is written to STDOUT. It will write the contents of the Unity log during the
# build, and it will write a keepalive every few seconds. Travis-CI will kill
# builds that have not written to STDOUT for 10 minutes. Sometimes Unity3D will
# not write any output to STDOUT for more than 10 minutes while building.

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

# If an arg was passed in, set TARGET to that arg.
if [ ! -z "$1" ]
then
  TARGET=$1
fi

# Ensure that TARGET was set, either via the env variable TARGET or by cmdline arg.
export TARGET=${TARGET:?"Please set TARGET via env or as an arg to one of linux, windows, osx, or webplayer."}

export PROJECT="planet-hero"
export PROJECT_PATH=$(pwd)
export LOG_PATH=$(pwd)/$TARGET.unity.log
export BUILD_PATH=$(pwd)/Build/$TARGET
export RELEASE_PATH=$(pwd)/Release

mkdir -p $BUILD_PATH $RELEASE_PATH
touch $LOG_PATH

echo "Forking build process for target: $TARGET"
./Scripts/Targets/$TARGET.sh &
build_pid=$!
echo "Build forked with PID: $build_pid"

echo "Logging build output to $LOG_PATH"
tail -f $LOG_PATH &

while true
do
  checkProcess $build_pid
  sleep 10
done
