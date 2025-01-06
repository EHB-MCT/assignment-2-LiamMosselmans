# Github Repository Structure

## Conclusion
Unity project in root of Github repo OR Unity project encapsulated into a folder.

## Repository Structure Convention
+---Documentation\
|   +---Research.md\
|   +---Progress.md\
|   ...\
+---UnityProject\
|   +---Assets\
|   +---ProjectSettings\
|   +---Packages\
+---README.md\
+---LICENSE\
...


# Naming Conventions
For clarity and readability, I will be using a mix of **camelCase** and **PascalCase**. Below is a breakdown of which naming style applies to which elements in the project:

## **camelCase**  
Used for the following elements:

- **Method Variables**  
  Example: `currentSpeed`, `jumpHeight`

- **Private Variables/Fields** (prefixed with "_")  
  Example: `_playerHealth`, `_isJumping`

- **Arguments & Parameters**  
  Example: `speed`, `targetPosition`


## **PascalCase**  
Used for the following elements:

- **Classes**  
  Example: `PlayerController`, `EnemyAI`

- **Methods**  
  Example: `CalculateScore()`, `UpdatePlayerState()`

- **Public & Protected Variables/Fields**  
  Example: `PlayerScore`, `EnemyHealth`

- **Interfaces** (prefixed with "I")  
  Example: `IDamageable`, `IWeapon`

- **Enums**  
  Example: `PlayerState`, `GameMode`


# Folder Structure
Unity also has special folders which hold core functionalities. These are Editor, Editor Default Resources, Gizmos, Resources, Standard Assets, and Streaming Assets,
but I will only be using the Assets folder which is present on project creation, and potentially the Editor folder for custom editor scripts.

## Content-Based Approach OR Feature-Based Approach
The content-based approach organizes folders by asset type, such as Animations, Scripts, Materials, and Prefabs. This structure is simple and works well for small projects or beginners. However, as the project grows, it can become disorganized since unrelated assets are grouped together. Developers may end up creating subfolders like Scripts/Player or Scripts/Enemies to manage the increasing complexity.

The feature-based approach structures folders by features, such as Player, Enemies, and UI. Each folder contains all related assets (e.g., scripts, animations, prefabs) for that feature. This approach keeps features self-contained and modular, making it easier to scale and collaborate on larger projects. However, it may result in duplicate assets if shared resources aren’t centralized.

For the goal of this assignment, I will be utilizing the content-based approach. My reasoning is that that the project will be on a smaller scale, and the content-based approach is also the standard that Unity itself adheres to. Thus this approach will make my project easy to navigate for anyone.

## Folder Structure Convention
Assets\
+---Art\
|   +---Materials\
|   +---Models\
|   +---Textures\
+---Audio\
|   +---Music\
|   +---Sound\
+---Animations\
...