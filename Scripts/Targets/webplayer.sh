#! /bin/sh

# Bail on errors
set -e

echo "Attempting to build $PROJECT for webplayer"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile $LOG_PATH \
  -projectPath $PROJECT_PATH \
  -buildWebPlayer $BUILD_PATH \
  -quit

# We want the html file to be served by default on most webservers, so rename it index.
echo "Moving $BUILD_PATH/$TARGET.html to $BUILD_PATH/index.html"
mv "$BUILD_PATH/$TARGET.html" "$BUILD_PATH/index.html"

# We want to serve out of an S3 bucket, so we leave it a directory for easy upload.
echo "Packaging $TARGET release into $RELEASE_PATH/$TARGET"
cp -r $BUILD_PATH $RELEASE_PATH
