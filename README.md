# ShiftGames_TestTask

### Task:
Make the RPG-like character equipment system. Download the unity project (v. 2020.3.2) from [here](https://drive.google.com/file/d/18Nv3uhhkz4eM-NsIa_SIS76G1IAVLTHF/view?usp=sharing). Open the Shift Games / Equipment system / Scenes / Equipment system scene. You can see the default character and HUD equipment panel with the equipped items and character stats. To save your time, the scene already contains all the elements needed to complete the task. You just need to write code:)

Character stats and model must react to changing the equipped items. If a character has no equipped items, his stats will be default. Any equipped item can modify a character's stats.

Do not take this task as a one-time task, but as part of a big game. Your system should be easy to read and modify. Make a simple way to add new or modify existing items. Inspectors on your components must be friendly for developers.

The Odin Inspector plugin is inside the project. It can help you to make your task faster, but using that plugin is optional. 

Meshes ID (Meshes should be taken from here: Assets/PolygonFantasyHeroCharacters/Models/ModularCharacters.fbx)

<table>
<thead>
<tr>
<th>Slot name</th>
<th>Head</th>
<th  colspan="4">Torso</th>
<th  colspan="2">Legs</th>
</tr>
</thead>
<tbody>
<tr>
<td>Part name</td>
<td>Head</td>
<td>Torso</td>
<td>Upper arms</td>
<td>Lower arms</td>
<td>Hands</td>
<td>Hips</td>
<td>Legs</td>
</tr>
<tr>
<td>None</td>
<td>03 no helmet</td>
<td>02</td>
<td>01</td>
<td>01</td>
<td>04</td>
<td>14</td>
<td>0</td>
</tr>
<tr>
<td>Leather</td>
<td>02 helmet</td>
<td>28</td>
<td>09</td>
<td>14</td>
<td>04</td>
<td>24</td>
<td>04</td>
</tr>
<tr>
<td>Iron</td>
<td>10 helmet</td>
<td>03</td>
<td>1</td>
<td>16</td>
<td>04</td>
<td>25</td>
<td>8</td>
</tr>
</tbody>
</table>

Base character stats
<table>
<tbody>
<tr>
<td>Armor</td>
<td>0</td>
</tr>
<tr>
<td>Strength</td>
<td>10</td>
</tr>
<tr>
<td>Agility</td>
<td>20</td>
</tr>
<tr>
<td>Maxspeed</td>
<td>20</td>
</tr>
<tr>
<td>convenience</td>
<td>5</td>
</tr>
</tbody>
</table>

Weapons
<table>
<thead>
<tr>
<th></th>
<th>Mace</th>
<th>Sword</th>
</tr>
</thead>
<tbody>
<tr>
<td>Armor</td>
<td>+1</td>
<td>+2</td>
</tr>
<tr>
<td>Strength</td>
<td>+4</td>
<td>+9</td>
</tr>
<tr>
<td>Agility</td>
<td>-5</td>
<td>-3</td>
</tr>
<tr>
<td>Max speed</td>
<td>-5</td>
<td>-3</td>
</tr>
<tr>
<td>convenience</td>
<td>+4</td>
<td>-4</td>
</tr>
</tbody>
</table>

Helmets
<table>
<thead>
<tr>
<th></th>
<th>Doctor’smask</th>
<th>Ironhelmet</th>
</tr>
</thead>
<tbody>
<tr>
<td>Armor</td>
<td>+1</td>
<td>+5</td>
</tr>
<tr>
<td>Strength</td>
<td>-</td>
<td>-</td>
</tr>
<tr>
<td>Agility</td>
<td>-1</td>
<td>-5</td>
</tr>
<tr>
<td>Max speed</td>
<td>-1</td>
<td>-3</td>
</tr>
<tr>
<td>convenience</td>
<td>+1</td>
<td>+2</td>
</tr>
</tbody>
</table>

Armors
<table>
<thead>
<tr>
<th></th>
<th>Leather armor </th>
<th>Iron armor </th>
</tr>
</thead>
<tbody>
<tr>
<td>Armor </td>
<td>+4 </td>
<td>+10 </td>
</tr>
<tr>
<td>Strength </td>
<td>- </td>
<td>- </td>
</tr>
<tr>
<td>Agility </td>
<td>-2 </td>
<td>-5 </td>
</tr>
<tr>
<td>Max speed </td>
<td>-3 </td>
<td>-5 </td>
</tr>
<tr>
<td>convenience </td>
<td>+4 </td>
<td>+3 </td>
</tr>
</tbody>
</table>

Pants
<table>
<thead>
<tr>
<th></th>
<th>Leather pants </th>
<th>Iron pants </th>
</tr>
</thead>
<tbody>
<tr>
<td>Armor </td>
<td>+3 </td>
<td>+5 </td>
</tr>
<tr>
<td>Strength </td>
<td>+1 </td>
<td>+1 </td>
</tr>
<tr>
<td>Agility </td>
<td>-2 </td>
<td>-5 </td>
</tr>
<tr>
<td>Max speed </td>
<td>-3 </td>
<td>-3 </td>
</tr>
<tr>
<td>convenience</td>
<td>+3</td>
<td>+2</td>
</tr>
</tbody>
</table>
