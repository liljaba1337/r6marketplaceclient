# r6marketplaceclient


The design is bad since I'm not a designer at all, but if someone wants to spend their time designing this project, I'll gladly accept any pull requests :3

## Planned features

- [ ] Basic functionality with increased performance
- [ ] Autosale of cheap items pulled from packs
- [ ] Ability to place more than 5 orders
- [ ] Multiaccount
- [ ] Autotrader

You can suggest something else by [opening an issue](https://github.com/liljaba1337/r6marketplaceclient/issues)

> [!NOTE]  
> I'm using my [R6S API wrapper](https://github.com/liljaba1337/r6-marketplace), so you'll need to clone it too (already referenced in the repo).

> [!WARNING]  
> Since all Ubisoft API methods require authentication, you will need to enter your Ubisoft credentials for this program to work. Nevertheless, you don't need to have marketplace access or own the game unless you plan to use the buy/sell features. If you're not using those, I recommend creating an alt account to use this app instead of logging in with your main one.

> [!WARNING]  
> Your credentials and authentication tokens are encrypted using `Windows Data Protection API (DPAPI)` and stored in `data/secret.dat`. The data is only decrypted and held in memory briefly, minimizing exposure and helping keep your credentials secure. While Microsoft ensures that this encryption cannot be broken without direct access to your machine, it is still recommended that you handle this file with care.

![screenshot](.github/screenshots/Screenshot_88.png)
