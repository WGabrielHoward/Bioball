# Bioball

A 3D rolling survival game built in Unity where you control a plant sphere, dodging enemies, collecting gems, and racing to reach the next level.

**Play it now in your browser:**  
[Play Bioball on Unity Play](https://play.unity.com/en/games/3d1ab859-707c-44e6-90b9-8dddc777cf41/tumbleweed-tales)

<!-- Replace with gameplay GIF -->
<img src="Assets/gameplay/Bioball_v1.gif" alt="Gameplay GIF" width="600"/>

### Gameplay & Controls
- **Objective**: Roll towards the goal, collect gems for points, avoid enemies, and reach the goal to advance levels.
- **Movement**:
  - **W / Up Arrow** — Roll forward (add force)
  - **S / Down Arrow** — Roll backward
  - **A / D** or **Left / Right Arrows** — Rotate camera / steer
- Enemies chase—survive by outrolling them!
- High score persists between sessions.

### Features
- Clean OOP architecture (polymorphism, inheritance, encapsulation, abstraction)
- Reusable `DamageOverTime` effects decoupled from enemies
- Persistent high score via JSON serialization
- Physics-based rolling movement
- Multiple levels with increasing difficulty
- WebGL build hosted on Unity Play

### Tech Stack
- Unity 6 (6000.2.8f1)
- C#
- Physics-driven gameplay
- Data persistence
- Iterative WebGL deployment

### How to Run Locally
1. Clone: `git clone https://github.com/WGabrielHoward/Bioball.git`
2. Open in Unity Hub (6000.2.f1 or compatible)
3. Load the main scene and press Play!

### Screenshots
<img src="Assets/gameplay/screenshot3.png" alt="FreezingScreenshot" width="400"/>
<img src="Assets/gameplay/screenshot2.png" alt="PauseScreenshot" width="400"/>
<img src="Assets/gameplay/screenshot1.png" alt="ClearMemoryScreenshot" width="400"/>

### License
This repository is licensed under the **GNU GPL-3.0 License**, with the following exception:
- **Lore and Backstory Files**: All files located in the `lore/` directory are copyrighted and are not covered under the GNU GPL-3.0 License. Redistribution, modification, or use of these files is prohibited without explicit written permission. For details, see `lore/COPYRIGHT.md`.

Built by [William G. Howard](https://www.linkedin.com/in/william-g-howard) while studying Game Programming at DePaul University.
