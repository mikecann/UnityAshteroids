![enumerate resources](http://i.imgur.com/xO3bydA.png)

Unity Ashteroids
=============



Enumerate Resources
===================

![enumerate resources](http://i.imgur.com/OlkmYR6.png)

Enumerate Resources is a handy util for creating type-safe resource references. Traditionally you have to manually create constant strings to load resources at runtime:

```
Resources.Load("Prefabs/Cars/Porsche");
```

This is fragile. If the asset is moved you wont know about the crash until you run the game, this line of code may not be executed often and hence introduces a bug that may only present itself at a later date.

Enumerate Resources scans a resources directory and generates a type-safe alternative:

```
Resources.Load(GameResources.Prefabs.Cars.Porsche);
```

Now if you move the resource and run the enumerator you will get a compile error.

For added sugar there is a method to add the loaded resource as a child of a game object (handy for prefabs):

```
obj.LoadChild(GameResources.Prefabs.Icons.IndicatorArror);
```

There are many other utils and extensions, and more to come.

Checkout the source for more info: https://github.com/mikecann/Unity-Helpers/tree/master/Scripts

Tests
=====

I have included a number of Unit Tests with the project. If you would like to run the tests yourself then just make sure you include the "Unity Test Tools" project from the asset store.

Installation
============

Simply include the source in your project, to use the extension methods dont forget to include the namespace:

```
using UnityHelpers;
```