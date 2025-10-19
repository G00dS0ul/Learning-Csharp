# Learning C#

A personal collection of C# exercises, sample projects, and experiments by G00dS0ul.

This repository is intended to collect small learning projects and examples that demonstrate C# language features, .NET APIs, and common patterns. Each subfolder contains one focused project or exercise with its own README and instructions when needed.

---

## Table of Contents

- <a>About</a>
- <a>Repository structure</a>
- <a>Requirements</a>
- <a>Getting started</a>
- <a>How to run a project</a>
- <a>Contributing</a>
- <a>Clean-up / maintenance tips</a>
- <a>License</a>
- <a>Contact</a>

---

## About

This repo is a learning hub for C# and .NET. Projects are intentionally small and focused so you can read and run them quickly. Each project should compile with a modern .NET SDK (see Requirements).

---

## Repository structure

Use this as a guideline for organizing new exercises and projects:

- /src/ - primary project folders (one folder per project)
- /examples/ - short snippets and examples
- /docs/ - additional documentation or design notes
- /tests/ - unit tests, when applicable

Each project folder should include:
- A short README describing its purpose and how to run it
- A project file (.csproj)
- No committed binaries (use .gitignore to exclude /bin and /obj)

Example:
- src/HelloWorld/
  - HelloWorld.csproj
  - Program.cs
  - README.md

---

## Requirements

- .NET SDK (e.g., .NET 7 or later) — install from https://dotnet.microsoft.com/download
- Optional: Visual Studio, Visual Studio Code + C# extension, JetBrains Rider

Check your installed SDK with:
```
dotnet --version
```

---

## Getting started

1. Clone the repo:
```
git clone https://github.com/G00dS0ul/Learning-Csharp.git
cd Learning-Csharp
```

2. Inspect projects in `src/` and run one:
```
cd src/HelloWorld
dotnet run
```

---

## How to run a project

Most projects are runnable with `dotnet run`. If a project is a library, look for its test or sample runner inside the folder.

Example minimal Program.cs
```
csharp
using System;
class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, C#!");
    }
}
```

---

## Contributing

If you're adding a new exercise or project:
- Create a new folder under `src/` with a descriptive name.
- Add a short README.md in that folder explaining the goal and how to run the code.
- Keep each project focused and small.
- Add tests under `tests/` when helpful.
- Follow consistent naming and C# style (consider using dotnet format).

Suggested PR workflow:
1. Create a branch: `git checkout -b feat/add-new-exercise`
2. Make your changes and commit: `git commit -am "Add: <short description>"`
3. Push and open a PR to `main`.

---

## Clean-up / maintenance tips

- Add or update `.gitignore` to exclude IDE folders, /bin, /obj, *.user files, and other generated files. Example entries:
```
/bin/
/obj/
/.vs/
*.user
*.suo
```
- Remove committed binaries and build artifacts: delete /bin and /obj from the repo and commit the deletions.
- Consider adding a `global.json` to pin the SDK version if your projects require a specific .NET SDK.
- Run `dotnet restore` and `dotnet build` for each project to ensure clean builds.
- Use `dotnet format` to apply consistent formatting.
- If you have multiple projects, consider adding a solution file (`.sln`) to make opening the entire collection easier.
- Add a LICENSE (e.g., MIT) to clarify reuse rules.

---

## License

This repository is available under the MIT License. See LICENSE for details.

---

## Contact

Created by G00dS0ul — https://github.com/G00dS0ul

If you'd like feedback, open an issue or PR with a proposed change.
