Here what I managed to do in 4 hours.

Basic gameplay seem to work. But it is extremely laggy. From quick glance at a profiler it looks like problem is with recalculating path each time a unit want to move.

Also, it may be related to the fact that map is huge if we count hexes.

Anyway, by using ready asset from asset store we can quickly start with something and iterate from that.

For hexes and pathfinding I used this asset https://assetstore.unity.com/packages/tools/level-design/prototiles-turn-based-map-creator-209087

Gameplay systems are quite simple. We have GameManager which sets up units. Units move and attack.
There are configs for units and teams. Menu to set seed. Some animations to visualize death and damage.


