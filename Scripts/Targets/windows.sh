#! /bin/sh

# Bail on errors
set -e

echo "Attempting to build $PROJECT for Windows"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $LOG_PATH \
  -projectPath $PROJECT_PATH \
  -buildWindowsPlayer "$BUILD_PATH/$PROJECT.exe" \
  -quit

echo "Removing debug *.pdb files from build assets"
cd $BUILD_PATH && rm *.pdb && cd -

RELEASE_ASSET=$RELEASE_PATH/$PROJECT-windows.zip

echo "Packaging $TARGET release into $RELEASE_ASSET"
cd $BUILD_PATH && zip -r $RELEASE_ASSET . && cd -
