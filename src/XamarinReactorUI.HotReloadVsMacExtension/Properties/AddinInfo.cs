//
// AddinInfo.cs
//
// Author:
//       Mikayla Hutchinson <m.j.hutchinson@gmail.com>
//
// Copyright (c) 2017 Microsoft Corp.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin(
    "HotReloadExtension",
    Namespace = "XamarinReactorUI",
    Version = "1.0"    
)]

[assembly: AddinName("ReactorUI for Xamarin Forms Hot Reload")]
[assembly: AddinCategory("IDE extensions")]
[assembly: AddinDescription("ReactorUI for XamarinForms Hot Reload Addin for Visual Studio for Mac")]
[assembly: AddinAuthor("adospace")]

// to create a package (.mpack), under XamarinReactorUI.HotReloadVsMacExtension directory, run :
// /Applications/Visual\ Studio.app/Contents/MacOS/vstool setup pack ./bin/Release/net471/XamarinReactorUI.HotReloadVsMacExtension.dll -d:./Versions


