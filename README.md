<div align="center">

# 🧛 Slot Survivor

**A top-down roguelite where fate spins on every level-up.**

Survive 10 minutes in a cursed gothic arena. Every level-up spins a slot machine —
land a jackpot for evolved weapons, or let bad luck cripple your build.

[![Play on itch.io](https://img.shields.io/badge/▶_PLAY_NOW-itch.io-FA5C5C?style=for-the-badge&logo=itchdotio&logoColor=white)](https://veraxec.itch.io/slot-survivor)

</div>

---

## 🎰 The Slot Machine

The heart of the game. Instead of picking upgrade cards, **every level-up spins three reels:**

| Result | Chance | Outcome |
|---|---|---|
| 🎰 **Jackpot** (3 match) | ~4% | Evolved weapon — bouncing bullets, piercing shots, multi-shot fan, or a high-stakes gamble |
| ✨ **Pair** (2 match) | ~48% | Strong stat boost in that symbol's category |
| 💀 **No match** | ~48% | Roll the dice — small bonus, or a **harsh downgrade** with no mercy |

Bad runs make you want to spin again. That's the hook.

## ✨ Features

- ⏱️ Tight 10-minute runs ending in a boss fight
- 🎲 Real risk-reward — downgrades are permanent, every run is different
- 👾 3 enemy types + boss, with spawn rates that scale per level
- 🩸 Juicy feedback — hit flashes, damage numbers, screen shake, death particles
- 🌐 Plays right in your browser

## 🛠️ Built With

`Unity 6.3 LTS` · `C#` · `DOTween` · `Cinemachine 3.x`

## 🧩 Technical Highlights

- **Object Pooling** — projectiles, enemies and XP orbs are pooled; zero `Instantiate`/`Destroy` during gameplay, no GC spikes
- **Event-driven architecture** — systems communicate through a static `GameEvents` hub (Observer pattern), no hard references between them
- **ScriptableObject data** — enemy and upgrade stats live in data assets; adding content needs no code changes
- **State machine AI** — extensible `BaseEnemy` base class with inherited variants
- **Procedural map generation** — seed-based generator for the gothic arena layout

## 🎮 Controls

| Action | Input |
|---|---|
| Move | WASD |
| Attack | Automatic (targets nearest enemy) |
| Confirm slot | Left Click |
| Pause | ESC |

## 🚀 Running Locally

Open with Unity 6.3 LTS, load `Scenes/MainMenu`, press Play.

---

<div align="center">
Made by <a href="https://github.com/barisorman">Barış Orman</a>
</div>
