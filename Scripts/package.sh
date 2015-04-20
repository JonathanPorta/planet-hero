#! /bin/bash

mkdir -p ./pkg

echo "Zipping up the Windows assets..."
zip ./pkg/planet-hero-windows.zip ./Build/windows/planet-hero.exe ./Build/windows/planet-hero_Data/

echo "Zipping up the OS X assets..."
zip ./pkg/planet-hero-osx.zip ./Build/osx/planet-hero.app/

echo "Tarballing up the Linux assets..."
tar -cvzf ./pkg/planet-hero-linux.x86_64.tgz ./Build/linux/

echo "Move web player build to the pkg directory for upload..."
mv ./Build/webplayer ./pkg/
