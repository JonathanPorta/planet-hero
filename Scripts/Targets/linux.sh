#! /bin/sh

# Bail on errors
set -e

echo "Attempting to build $PROJECT for Linux"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $LOG_PATH \
  -projectPath $PROJECT_PATH \
  -buildLinuxUniversalPlayer "$BUILD_PATH/$PROJECT" \
  -quit

RELEASE_ASSET=$RELEASE_PATH/$PROJECT-linux.x86_64.tgz

echo "Packaging $TARGET release into $RELEASE_ASSET"
cd $BUILD_PATH && tar -cvzf $RELEASE_ASSET . && cd -
