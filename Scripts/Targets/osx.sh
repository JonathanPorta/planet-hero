#! /bin/sh

# Bail on errors
set -e

echo "Attempting to build $PROJECT for OS X"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $LOG_PATH \
  -projectPath $PROJECT_PATH \
  -buildOSXUniversalPlayer "$BUILD_PATH/$PROJECT" \
  -quit

RELEASE_ASSET=$RELEASE_PATH/$PROJECT-osx.zip

echo "Packaging $TARGET release into $RELEASE_ASSET"
cd $BUILD_PATH && zip -r $RELEASE_ASSET "./$PROJECT.app" && cd -
