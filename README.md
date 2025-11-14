# r6marketplaceclient

For now, I'm using a compiled version of my [R6S API wrapper](https://github.com/liljaba1337/r6-marketplace), so I can make all the required changes without pushing it to NuGet every time, therefore you'll need to manually build and reference it.

The design is bad since I'm not a designer at all, but if someone wants to spend their time designing this project, I'll gladly accept any pull requests :3

## Planned features

- [ ] Basic functionality with increased performance
- [ ] Autosale of cheap items pulled from packs
- [ ] Ability to place more than 5 orders
- [ ] Multiaccount
- [ ] Autotrader

You can suggest something else by [opening an issue](https://github.com/liljaba1337/r6marketplaceclient/issues)

## How to build?
1. Clone or download the code of [r6-marketplace](https://github.com/liljaba1337/r6-marketplace), extract it somewhere, and build it.
2. Open `r6marketplaceclient.csproj`, find `<Reference Include="r6-marketplace">` and update `HintPath` to point to the `r6-marketplace.dll` you just built (usually found in `r6-marketplace\bin\Release\net8.0`).
3. Build the client and you're good to go!


![screenshot](.github/screenshots/Screenshot_88.png)
