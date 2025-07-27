---
sidebar_position: 1
---

# Installing the MDK

**MDK (Mod Development Kit)** is a mod development toolkit provided by DCCM. This tutorial will guide you through the installation and basic configuration of the MDK.

## Prerequisites

- **.NET 9 SDK** ([Download Link](https://dotnet.microsoft.com/en-us/download/dotnet/9.0))
  - (Optional) Visual Studio 2022
- [DCCM Core Files](/docs/tutorial/install-core)

## Installation Steps

### Running the MDK Installation Script

- Open **File Explorer** and navigate to the `coremod/core/mdk` folder in the game's root directory.
- Right-click on the `install.ps1` PowerShell script and **Run with PowerShell**.

## Verifying the MDK Installation

Execute the following command in PowerShell or Command Prompt:

```bash
dotnet nuget list source
```

You should see something similar to the following in the output:

```text
Registered Sources:
  1.  DeadCoreModdingMDK [Enabled]
```
