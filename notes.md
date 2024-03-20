# Chat with Nick

## Tasks/Tests/Checks
- rename a Test to **Check** because they're checking off things as they progress
### Attributes
  - Id -> for database and external reference
  - IsHidden (by default, maybe saved for each user?)
  - IsMandatory (raises alert if not fufiled)
  - Type of check
    - Checkmark (one, or multiple with labels)
    - Text (short)
    - Text (long)
    - Drop-down list
    - Number Value
    - formula
    - Date
  - Reference (a value this chekc is compared to: another task,a  set value, etc)
  - Tolerance (determines if a check passed based on what the Reference is)
  - Notes
### Component Design
- Display Mode:
  - Reorganize (up/down)
  - Hide
  - Remove
  - Coloured based on passing tolerance
- Edit Mode:
  - configure Check setup
  - layout depends on Check Type
  - change reference/tolerance/isMandatory  
  - copy (other Check components show a copy symbol to copy/paste)
  - referenceID (show for all related Checks to reference them)
## Check Templates
- a Check designed in advance to be copied when importing a GroupTemplate or PatientTemplate
### Attributes
- Name/ID
- Default Background Color
- list of **Checks** 
- isHidden by default
### Component Design
- Display Mode
  - Each CheckComponent listed
  - Hide/Show/Collapse
- Edit Mode:
  - Background colour
  - Priority order (higher/lower) or draggable
  - reorganize Checks
  - Duplicate (copy and paste)
  - Add new Check
## Patient Templates
A patient template can be selected and applied when a patient is first added
### Attributes
- Name/ID
- List of **CheckGroups**
### Component Design
Lists the CheckGroups
## Patient
### Attributes
- Patient Name
- Patient ID
- Date
- NP Date
- List of **CheckGroups**

# Patient Example
## Patient Info
- **Name** Smith, John
- **Patient ID**: AG08976
- **Date** 0903 30-Jan-2024
- **Treatment Date** Monday 31-Jan-24
## Standard Info
- Site and Diagnosis: (text short) -> Brain
- Treatment Intent: (drop-down list) -> Palliative, Curative, Adjuvant, Neo-Adjuvant
- Treatment Description: (text short) -> GBM
- Plan Name: (text short) -> LBra_RO
- Check Type: (drop-down) -> VMAT, 3DCRT, Electron, SABR, IMRT, DIBH, Hybrid
- Prescription Dose (cGy): (number) -> 4000
- Prescription #Fractions: (number) -> 15
- Replan?: (checkbox) -> yes[], no[x]
- Previous RT?: (checkbox) -> yes[], no[x]
- Notes of previous treatment: (text long)
- Con Chemo?: (checkbox) -> yes[], no[x]
- Type: (text short)
## Pre-RO Check
- PAN/Req/Directives: (checkbox) -> []
- Patient Immobilization: (drop-down list) -> Shell, BreastB, Solstice, SBRT
- All OARs Contoured?: (checkbox) -> []
- Contour QA: (checkbox) -> []
- Registration: (checkbox) -> [] N/A[]
- BODY Check (+FOV Artifact): (checkbox) -> []
- Prothesis or artifact?: (checkbox) -> yes[], no[x]
- Pacemaker?: (checkbox) -> yes[], no[x]
