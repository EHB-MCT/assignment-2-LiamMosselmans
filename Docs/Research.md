# Github Repository Best Practices 

## Sources
- [Pacman Agent AI - Repository Example](https://github.com/ma-shamshiri/Pacman-Game#readme)
- [CubeWorld Game - Repository Example](https://github.com/federicodangelo/CubeWorld)

## Conclusion
Unity project in root of Github repo OR Unity project encapsulated into a folder.

### Final Repository Structure
+---Documentation\
|   +---research.md\
|   +---progress.md\
|   ...\
+---UnityProject\
|   +---Assets\
|   +---ProjectSettings\
|   +---Packages\
|   ...\
+---README.md\
+---LICENSE
...\

# Folder Structure

## Sources
- [*Mastering Unity Game Development with C#* - A Book By Mohamed Essam](https://www.packtpub.com/en-dk/product/mastering-unity-game-development-with-c-9781835466360?srsltid=AfmBOop1nqcaIWT_lxCmxtXIEoL_QOsY9NktALcZQc_sNNZSZ817YCaF)
- [Best practices for organizing your Unity project - Unity Documentation](https://unity.com/how-to/organizing-your-project)
- [Project Structure - Reddit Post](https://www.reddit.com/r/unity/comments/192qk76/project_structure/)
- [A guide to folder structures for Unity 6 projects - Website Article](https://www.anchorpoint.app/blog/unity-folder-structure)
- [A Brief Anatomy of A Unity Project Folder - Website Article](https://medium.com/@jsj5909/a-brief-anatomy-of-a-unity-project-folder-563bd3f4ad40)
- [Folder structuring - Unity Community Post](https://discussions.unity.com/t/folder-structuring/815725)
- [How do you people organize folders into your project? - Unity Community Post](https://discussions.unity.com/t/how-do-you-people-organize-folders-into-your-project-im-trying-to-find-what-is-best-for-me/942296)
- [Unity Folder Structure - YouTube Video By DVS Devs](https://www.youtube.com/watch?v=Qf6VHfOUkSQ)
- [Organizing Your Unity Project — Content vs Feature Folders - YouTube Video By Infallible Code](https://youtu.be/o8HIGKObG1Q?si=SVLAYjzRygOqCuv-)

## Conclusion
Unity also has special folders which hold core functionalities. These are Editor, Editor Default Resources, Gizmos, Resources, Standard Assets, and Streaming Assets,
but I will only be using the Assets folder which is present on project creation, and potentially the Editor folder for custom editor scripts.

### Content-Based Approach OR Feature-Based Approach
The content-based approach organizes folders by asset type, such as Animations, Scripts, Materials, and Prefabs. This structure is simple and works well for small projects or beginners. However, as the project grows, it can become disorganized since unrelated assets are grouped together. Developers may end up creating subfolders like Scripts/Player or Scripts/Enemies to manage the increasing complexity.

The feature-based approach structures folders by features, such as Player, Enemies, and UI. Each folder contains all related assets (e.g., scripts, animations, prefabs) for that feature. This approach keeps features self-contained and modular, making it easier to scale and collaborate on larger projects. However, it may result in duplicate assets if shared resources aren’t centralized.

For the goal of this assignment, I will be utilizing the content-based approach. My reasoning is that that the project will be on a smaller scale, and the content-based approach is also the standard that Unity itself adheres to. Thus this approach will make my project easy to navigate for anyone.

### Final Folder Structure Convention
Assets\
+---Art\
|   +---Materials\
|   +---Models\
|   +---Textures\
|   ...\
+---Audio\
|   +---Music\
|   +---Sound\
|   ...\
+---Scripts\
+---Prefabs\
+---Scenes\
+---UI
...\

# Naming Conventions

## Sources
- [*Mastering Unity Game Development with C#* - A Book By Mohamed Essam](https://www.packtpub.com/en-dk/product/mastering-unity-game-development-with-c-9781835466360?srsltid=AfmBOop1nqcaIWT_lxCmxtXIEoL_QOsY9NktALcZQc_sNNZSZ817YCaF)
- [Naming and code style tips for C# scripting in Unity - Unity Documentation](https://unity.com/how-to/naming-and-code-style-tips-c-scripting-unity)
- [C# Coding Conventions, The Better Way of Writing Your Code in Unity - Website Article](https://shakiroslann.medium.com/c-coding-conventions-the-better-way-of-writing-your-code-in-unity-6a2d574e7c2a)
- [Clean up your code: How to create your own C# code style - Unity Blog Article](https://unity.com/blog/engine-platform/clean-up-your-code-how-to-create-your-own-c-code-style)
- [C# naming conventions for Unity - Unity Community Post](https://discussions.unity.com/t/c-naming-conventions-for-unity/475961)
- [C# Coding Conventions in Unity - YouTube Video By iHeartGameDev](https://www.youtube.com/watch?v=vYIM-PG85vo)

## Conclusion
camelCase OR PascalCase
For clarity and readability I'll be using a mix of camelCase and PascalCase. I'll be using camelCase for the following: method variables, private variables/fields (prefixed with: "_"), arguments, parameters. PascalCase will be used for the following: classes, methods, public- & protected variables/fields, interfaces (prefixed with: "I"), Enums.


# Unity General Best Practices

## Sources
- [*Mastering Unity Game Development with C#* - A Book By Mohamed Essam](https://www.packtpub.com/en-dk/product/mastering-unity-game-development-with-c-9781835466360?srsltid=AfmBOop1nqcaIWT_lxCmxtXIEoL_QOsY9NktALcZQc_sNNZSZ817YCaF)
- [Unity Best Practices](https://github.com/SamuelAsherRivello/unity-best-practices)
- [Unity3D Good Practices](https://github.com/futurice/unity-good-practices)
- [How to structure your Unity project (best practice tips)](https://gamedevbeginner.com/how-to-structure-your-unity-project-best-practice-tips/)
- [Unity Architecture for Noobs - Game Structure - YouTube Video By Tarodev](https://www.youtube.com/watch?v=tE1qH8OxO2Y)