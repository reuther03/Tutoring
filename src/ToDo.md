## CompetenceGroup endpoints:
### 1. CompetenceGroupsController:
- odpowiedzialny za CompetenceGroup i każde Competence pod tym konkretym CompetenceGroup

---
### 2. Endpointy:

---
>- GET `/competence-groups` - pobranie listy wszystkich CompetenceGroup (**_paginacja_**)
---
>- GET `/competence-groups/{id}` - pobranie konkretnego CompetenceGroup ✅✅✅✅✅✅✅✅
---
>- POST `/competence-groups` - dodanie nowego CompetenceGroup razem z listą Competence do niego
---
>- POST `/competence-groups/{id}/competences` - dodanie nowych Competence do istniejącego CompetenceGroup
---
>- DELETE `/competence-groups/{id}/competences/{id}` - usunięcie Competence z CompetenceGroup (tutaj dodatkowa walidacja, żeby nie można było usunąć Competence, który jest przypisany do jakiegoś Usera)<br>
>>➡️ albo event usuwający ten competence z wszystkich Userów
---
>- PUT `/competence-groups/{id}` - edycja CompetenceGroup razem z listą Competence do niego
---
  >- DELETE `/competence-groups/{id}` - usunięcie CompetenceGroup (tutaj dodatkowa walidacja, żeby nie można było usunąć CompetenceGroup, który ma przypisane jakieś Competence)
---