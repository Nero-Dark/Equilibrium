@echo off
@echo Processing...
@del *.bin
@del *.out
chem.exe < chem.inp > chem.out
surf.exe < surf.inp > surf.out
AdvancedEquil.exe < equil.inp > equil.out
initialconditions.exe
@echo Complete
