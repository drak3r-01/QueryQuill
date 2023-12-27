<div align="center">
<h1> QueryQuill</h1>

```markdown
  ______                                            ______             __  __  __ 
 /      \                                          /      \           |  \|  \|  \
|  $$$$$$\ __    __   ______    ______   __    __ |  $$$$$$\ __    __  \$$| $$| $$
| $$  | $$|  \  |  \ /      \  /      \ |  \  |  \| $$  | $$|  \  |  \|  \| $$| $$
| $$  | $$| $$  | $$|  $$$$$$\|  $$$$$$\| $$  | $$| $$  | $$| $$  | $$| $$| $$| $$
| $$ _| $$| $$  | $$| $$    $$| $$   \$$| $$  | $$| $$ _| $$| $$  | $$| $$| $$| $$
| $$/ \ $$| $$__/ $$| $$$$$$$$| $$      | $$__/ $$| $$/ \ $$| $$__/ $$| $$| $$| $$
 \$$ $$ $$ \$$    $$ \$$     \| $$       \$$    $$ \$$ $$ $$ \$$    $$| $$| $$| $$
  \$$$$$$\  \$$$$$$   \$$$$$$$ \$$       _\$$$$$$$  \$$$$$$\  \$$$$$$  \$$ \$$ \$$
      \$$$                              |  \__| $$      \$$$                      
                                         \$$    $$                                
                                          \$$$$$$                                 
```

<img src="logo.jpg" alt="QueryQuill Image" width="400" height="400">

*Generate by [![DALL-E 3](https://img.shields.io/badge/DALL--E%203-OpenAI-%233171E3)](https://openai.com)

[![.NET](https://img.shields.io/badge/.NET-8-blueviolet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-brightgreen)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![WPF](https://img.shields.io/badge/Application-WPF-green)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
[![Console Application](https://img.shields.io/badge/Application-Console-green)](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new)
[![Windows](https://img.shields.io/badge/Platform-Windows-informational)](https://www.microsoft.com/en-us/windows/)
[![Mac](https://img.shields.io/badge/Platform-Mac-informational)](https://www.apple.com/mac/)
[![Linux](https://img.shields.io/badge/Platform-Linux-informational)](https://www.linux.org/)
[![MIT License](https://img.shields.io/badge/License-MIT-orange)](https://opensource.org/licenses/MIT)

</div>

## Overview 🌐

QueryQuill is software developed on the .NET 8 platform with the goal of simplifying the creation of Google Dorks to enhance browser usage to its full potential.

It can run autonomously in console mode (using pre-built versions) on Windows, Mac, and Linux platforms without prerequisites. The GUI version is exclusively available for Windows, utilizing WPF technology, and can also be executed without installation.

You have the option to run and build your own version if you have the .NET 8 SDK installed.

## Disclaimers 📢

The software provided under the MIT License is offered "as is" without any warranty, express or implied. The user of this software acknowledges and agrees that the author and contributors are not liable for any direct, indirect, incidental, special, exemplary, or consequential damages, including, but not limited to, procurement of substitute goods or services, loss of use, data, or profits or business interruption, however caused and on any theory of liability, whether in contract, strict liability, or tort (including negligence or otherwise) arising in any way out of the use of this software, even if advised of the possibility of such damage.

The user assumes all responsibility for the use and potential consequences of this software. The author disclaims any responsibility for any improper or illegal use of the software by third parties.

By using this software, you agree to the terms of the MIT License and acknowledge the provided disclaimer.

## How to Use 🚀

## Build 🛠️

To build your version, make sure you have the .NET SDK installed.
Use `dotnet publish -c Release` in the WPF or console project folder and specify the target platform with `--runtime`. You can also include the runtime to make your application self-contained with `--self-contained` option. You will find your application in the bin directory of the published project.

For example:

- Publish the application for Windows in 64-bit version without runtime:

```bash
dotnet publish -c Release --runtime win-x64
```

- Publish a version for Linux in 32-bit with included runtime:

```bash
dotnet publish -c Release --self-contained --runtime linux-x86
```

| --Runtime {option}| Description         |
|-------------------|---------------------|
| win-x64           | Windows 64-bit      |
| win-x86           | Windows 32-bit      |
| linux-x64         | Linux 64-bit        |
| linux-x86         | Linux 32-bit        |
| linux-arm         | Linux ARM           |
| linux-arm64       | Linux ARM 64-bit    |
| osx-x64           | macOS 64-bit        |
| osx-arm64         | macOS ARM 64-bit    |
| win-arm           | Windows ARM         |
| win-arm64         | Windows ARM 64-bit  |

## Contributing 🤝

If you would like to contribute to this project, please follow
the [GitHub Fork & Pull Request Workflow](https://gist.github.com/Chaser324/ce0505fbed06b947d962).

## Future Enhancements 🚀

- Improved user interface (Fluent Design)
- Enhancements the dork generator
- Implementation with MAUI

## Changelog 📜

## License 📄

<p>
    <a property="dct:title" rel="cc:attributionURL" href="https://github.com/drak3r-01/WebCrawl-Wizard">QueryQuill</a> by <a rel="cc:attributionURL dct:creator" property="cc:attributionName" href="https://github.com/drak3r-01">drak3r-01</a> is licensed under the <a href="https://opensource.org/licenses/MIT" target="_blank" rel="license noopener noreferrer" style="display:inline-block;">MIT License</a>.
</p>
