# Interface
Data disbatch function. 
Buttons for top 5, 10, 20. 
Input box for custom.
How to handle time grouping - should it be done in BBT or should it be done by manipulating finish times? Should it be handled? Displaying a warning by color if time is between 1.05 and 1.15 seconds - when trying to disbatch data with worrysome deltas it should show a warning box reminding to check the footage and change accordingly in BBT.
It should probably to possible to change the warning to either same timegroup or different timegroup - so then disbatching we only get a warning reminding us to change the timegrouping if it is neccesary. This could go both ways. (Finish time manipulation should be possible if it is only used to connect two different group as it would not influence the result.)


# Lynx/ST
## Implement data loading from sportstiming.dk/api/v1.1
Retrieve list of races from server and create a browse function. 
Or just accept event id as input. 

How to handle participant and race data from json

### Generate corresponding .ppl and mapfile
Mapping should probably be done either by UCI or signup ID. 

How to handle races where the same bib might be used by multiple people. The solution might be to append race shorthand ie. A101 for rider 101 in A.



# TCP
## General T&S tcpclient emulator
## Data disbatch 
## Retrieve status from tcplistener ie. BlackBoxTiming



