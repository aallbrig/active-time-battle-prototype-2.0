# Active Time Battle 2.0
I want to build an active time battle system like one you would see in Final Fantasy. To that end, I've written [two](https://play.unity.com/mg/other/active-time-battle-atb-simulation-prototype) (buggy) [games](https://play.unity.com/mg/other/atb-v0-6-0).

I'm starting from scratch because I think I can do it much better now and in a shorter amount of time.

### Goals
* Think up wieldable abstractions for scriptable objects. Yes, FloatVar or Vector3Var is great but does add tedium. A fighter or spawn point scriptable object seems to be at a better level of abstraction.
* Explore custom editor scripts. I want to be able to click an event scriptable object and broadcast it from the unity inspector.
* Build for, whats the right term... Debuggability? For example, I want systems to broadcast events to allow me to also manually broadcast events to see game behavior.
