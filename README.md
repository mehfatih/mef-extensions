mef-extensions
======
This project is solving the problem of removal [Managed Extensibility Framework (MEF)](https://msdn.microsoft.com/en-us/library/dd460648(v=vs.110).aspx) project plugin.

## Problem
MEF does not allow you to remove plugins(dll files) while the process is running. The reason is that the process use dll while it is running and operating system does not allow you to remove these files. Consequently, you can not remove plugins, you can add only.

## Solution
Solution of the problem were managed [mef](https://msdn.microsoft.com/en-us/library/dd460648(v=vs.110).aspx) plugings with a yaml file.

The content of sample [yaml](http://www.yaml.org) file is as follows :
```
plugins-dir : .\plugins
plugins:
 - a.dll
 - b.dll
```

## Contact
#### Developer
  * e-mail : mehmetfatihozd@gmail.com
