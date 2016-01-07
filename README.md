# DnD-Pointbuy-Extended
A flexible point buy calculator for Dungeons and Dragons 5th edition

[Screenshot 1](https://github.com/DrHemroid/DnD-Pointbuy-Extended/blob/master/DnD%20Point%20Buy/DnD%20Point%20Buy/screenshots/screenshot_main.png)
[Screenshot 2](https://github.com/DrHemroid/DnD-Pointbuy-Extended/blob/master/DnD%20Point%20Buy/DnD%20Point%20Buy/screenshots/screenshot_infotab.png)

DESCRIPTION:
This program is a point-buy calculator for Dungeons and Dragons 5th edition.
It is capable of following the rules stated in the Player's Handbook exactly, but it is more flexible and can go beyond the standard limits.

Adjustable settings include:
Total number of Ability Points (standard is 27, default in extended is 30)
The minimum and maximum ability score allowed (standard is 8 to 15, default in extended is 6 to 18)

These settings can be further changed by enabling "MinMax mode," where the minimum ability score is 3, and maximum is 20.
MinMax mode also allows for custom racial bonus stats, from -10 to +10, instead of the standard 0 to 2 of basic races.


TECHNICAL INFO:
The program is written in C# and is intended for Windows operating systems.
Made using Visual Studio 2013
Tested on Windows 7 sp1
No installation necessary. Double click to run the program.


KNOWN ISSUES ("Features"):
-When switching modes (Standard, Extended, MinMax) the user's stat points will reset. This is a lazy way of preventing a crash if the user has chosen incompatible stats for the new mode.
-When switching to MinMax mode, the racial bonuses "unlock" to -10 to +10, but they never get "re-locked" to +0 to +2 when switching back.
-The mousewheel and arrow keys change the numbers of the selected textbox.
-Pressing tab will select some of the "things" in a "random" order. I was too lazy to fix the tabIndex properties, but it should work as expected for Base and Racial-Bonus text boxes.
-Resizing the window makes it flicker. I'm too lazy to look into why this happens.
-No option to save your stats when you're done.
-C# is a dumb language and making it windows-exclusive is dumb.
-The UI is ugly and the colors don't make sense. It looks like it was made in a day [because it was =)].
-This would be better as a website, app, etc.


