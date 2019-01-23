**Om**

_app-common-form_-komponentet og det tilhørende stilarket _formStyles.css_ tilbyr en kontainer med tittel (tittel er valgfritt) for former og ett sett med klasser for stilering av form-kontrollene. Hensikten er å kunne få enhetlige former med samme stiluttrykk og å kunne gjøre formene mer vedlikeholdbare og enkle å utvide og endre siden endringen dermed kun trengs å gjennomføres på ett sted.

**Setup**
1. Plasser komponentfilene et passende sted: common-form.component.html og common-form.component.ts 
1. Legg til 'CommonFormComponent' i declarations-listen til app.module.ts
1. Legg stilarket formStyles.css et passende sted
1. Importer stilarket til det globale stilarket (src/styles.css): @Import 'FILSTI_TIL_FORMSTYLES_CSS'

**Bruk**

Legg formen eller form-komponentet som et barn av 'app-common-form'-komponentet. 
* En tittel angis ved å sette header-attibutten til ønsket tittel. Tittel er ikke obligatorisk
* Et input-element kan stileres med klassen formInput. 
* Knapper kan stileres med formButton. 
* p-tagger kan stileres med formError eller formMessage for å stileres med tanke på visning av feilmeldinger eller informasjonsmeldinger (slik som en beskjed som opplyser om at formen ble sendt inn).

_Eksempel_
```html
<app-common-form header="Legg til bruker">
  <form>
   <input placeholder="navn"  class="formInput"/>
   <input placeholder="tlf"   class="formInput"/>
   <input placeholder="email" class="formInput"/>

   <p class="formError"></p>
   <p class="formMessage"></p>
   <button type="submit" class="formButton">Send inn</button>
   <button type="button" class="formButton">Avbryt</button>
  </form>
</app-common-form>
```
