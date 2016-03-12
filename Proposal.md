# Competitive Tower Defense Game
## Target Platform

Android, and possibly iOS in the fugure

## Game Description

This is a two player competitive game. In this game, the players use resources to build offensive units, defensive units, or resource factories. The offensive units will go down certain routes and try to reach the other player’s home, and if they do, their owner scores. The defensive units can be built at building points. They will try to stop the incoming hostile offensive units. Players gain resources by building resource factories. The game ends when either a player has 10 points or when the time is up.

## Game Objects:

* Home: The thing you have to defend. It serves as a spawn point. If an offensive unit reaches the enemy home, its owner gets a point.
* Offensive Units: When a player builds an offensive unit, he must specify its spawn point and target. The target can be either an uncontrolled building point or the enemy home. Once the target building point is controlled by a player, the target will automatically be changed to the enemy home. (The player can not change the target of an offensive unit). Offensive units will respawn after some time when they die or reaches their target. (To make sure late game won’t be dominated by defensive strategy).
 * Unit that is cheap. Will stop to attack enemy offensive units. (but won’t chase them)
 * Unit that runs fast
 * Unit that is tanky
 * Unit that heals/enhances other friendly units
* Defensive Units:
 * Tower that fires arrows
 * Tower that fires mud balls
 * Tower that does AoE damage
 * Tower that heals friendly offensive units
 * (Barrack)Tower that can spawn offensive units
* Factories: Factory that generates gold. Can be upgraded?
* Building points: Points on the map where players can build defensive units or factories. A building point is always beside at least one path. If a player has three more units reaching an uncontrolled building point than his opponent, he gains control of the building point, and can build a defensive unit or factory on it.
* Path: Paths where the offensive units move on. All paths are connected to the homes.
Spawn point: Places on the path where the attacking units can spawn. The home is a spawn point. If a friendly (barrack) is adjacent to a path, there will be a spawn point on that grid of path, too.
* Projectiles:
 * Arrow: Deals plain damage to the enemy hit.
 * Mud ball: When hits an enemy, leaves a mud puddle on the ground that lasts for some time.
* Terrains:
Mud puddle: Has an owner. Slows down passing enemy offensive units.
## Game Control

The user uses the touch input to change his current focus. If the player selects an empty building point he controls, there will be a menu of buildings he can choose to build from. If the player selects a spawning point he controls, there will be a menu of attacking units he can choose to build from. After the player chooses which attacking unit to build, he chooses its target again with touch input.