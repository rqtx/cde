**Criar log**  
----  
Cria um log para um determinado sistema. Apenas roles System podem criar logs.  
  
* **URL**  
  
	api/log 
  
* **Method:**  
  
	`POST`  
  
* **URL Params** 
  
	**Required:**  
  
	Nenhum  
  
* **Data Params**  
	`title: text`  
	`details: text`,  
	`systemName: text`  
	`levelName: text` 
	`createdAt: DateTime|Optinal`
  
* **Success Response:**  
  
	* **Code:** 201 <br />  
  
* **Error Response:**  

	* **Code:** 400 BADREQUEST <br />  
	**Content**: `{ "Level/System does not exist" }`  
	  
	* **Code:** 401 UNAUTHORIZED <br />  
	  
	* **Code:** 403 FORBIDDEN <br /> 

**Retornar todos os logs**
----
Retorna uma vetor json com todos os log de um sistema.

* **URL**

	/api/log/system/:id  

* **Method:**

  	`GET`
  
*  **URL Params**

   **Required:**
 
   `id=[integer]`

   **Optional:**
 
  	sortby: title, date or level
  	orderby: asc or desc
  	page: interger
  
* **Data Params**

  	Nenhum

* **Success Response:**
  

  * **Code:** 200 <br />
    **Content:** `[
    {
        "id": 2,
        "title": "test1",
        "details": "test1 details",
        "createdAt": "0001-01-01T00:00:00",
        "systemName": "VavilovSeed",
        "levelName": "Critical"
    }
]`
 
* **Error Response:**

  * **Code:** 401 UNAUTHORIZED <br />  
  
  * **Code:** 403 FORBIDDEN <br />

**Retornar todos os logs de um level**
----
Retorna uma vetor json com todos os log de um sistema de apenas um level de log.

* **URL**

  	/api/log/system/:id/level/:id

* **Method:**

  	`GET`
  
*  **URL Params**

   **Required:**
 
   `id=[integer]`

   **Optional:**
 
  	sortby: title, date or level
  	orderby: asc or desc`
  	page: interger
  
* **Data Params**

  	Nenhum

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `[
    {
        "id": 2,
        "title": "test1",
        "details": "test1 details",
        "createdAt": "0001-01-01T00:00:00",
        "systemName": "VavilovSeed",
        "levelName": "Critical"
    }
]`
 
* **Error Response:**

  * **Code:** 401 UNAUTHORIZED <br />  
  
  * **Code:** 403 FORBIDDEN <br />

**Retornar log**
----
Retorna um json com os dados do log de um sistema.

* **URL**

  	/api/log/:id

* **Method:**

  	`GET`
  
*  **URL Params**

   **Required:**
 
   `id=[integer]`
  
* **Data Params**

  	Nenhum

* **Success Response:**
  

  * **Code:** 200 <br />
    **Content:** `{
        "id": 2,
        "title": "test1",
        "details": "test1 details",
        "createdAt": "0001-01-01T00:00:00",
        "systemName": "VavilovSeed",
        "levelName": "Critical"
    }`
 
* **Error Response:**

  * **Code:** 401 UNAUTHORIZED <br />  
  
  * **Code:** 403 FORBIDDEN <br />

  * **Code:** 404 NOTFOUND <br />  
        **Content**: `{ error = "Log not found" }`

**Retornar overview dos log**
----
Retorna um vetor json com o overview todos os log de um sistema.

* **URL**

  	/api/log/overview/:id

* **Method:**

  	`GET`
  
*  **URL Params**

   **Required:**
 
   `id=[integer]`
 
* **Data Params**

  	Nenhum

* **Success Response:**
  

  * **Code:** 200 <br />
    **Content:** `[
    {
        "log": {
            "id": 1,
            "title": "test1",
            "details": "test1 details",
            "createdAt": "0001-01-01T00:00:00",
            "systemName": "VavilovSeeds",
            "levelName": "Critical"
        },
        "events": 1
    }
]`
 
* **Error Response:**

  * **Code:** 401 UNAUTHORIZED <br />  
  
  * **Code:** 403 FORBIDDEN <br />

**Deletar Log**  
----  
Deleta um log.  Apenas a role Admin pode executar essa ação.
  
* **URL**  
  
	/api/log/:id  
  
* **Method:**  
  
	`DELETE`  
  
* **URL Params**  
  
	**Required:**  
  
	`id=[integer]`  
  
* **Data Params**  
	Nenhum  
  
* **Success Response:**  
  
  
	* **Code:** 200 <br />  
  
  
* **Error Response:**  
  
	* **Code:** 401 UNAUTHORIZED <br />  
	  
	* **Code:** 403 FORBIDDEN <br />  
  
	* **Code:** 404 NOTFOUND <br />  
	**Content**: `{ error = "Log not found" }`

**Deletar Logs pelo level**  
----  
Deleta logs de um sistema pelo level.  Apenas a role Admin pode executar essa ação.
  
* **URL**  
  
	/api/log/system/:systemId/level/:levelId
  
* **Method:**  
  
	`DELETE`  
  
* **URL Params**  
  
	**Required:**  
  
	`systemId=[integer]` 
	`levelId=[integer]` 
  
* **Data Params**  
	Nenhum  
  
* **Success Response:**  
  
  
	* **Code:** 200 <br />  
  
  
* **Error Response:**  
  
	* **Code:** 401 UNAUTHORIZED <br />  
	  
	* **Code:** 403 FORBIDDEN <br />  

**Deletar Log**  
----  
Deleta um log.  Apenas a role Admin pode executar essa ação.
  
* **URL**  
  
	/api/log/system/:systemId  
  
* **Method:**  
  
	`DELETE`  
  
* **URL Params**  
  
	**Required:**  
  
	`systemId=[integer]`  
  
* **Data Params**  
	`date: DateTime`  
  
* **Success Response:**  
  
  
	* **Code:** 200 <br />  
  
  
* **Error Response:**  
  
	* **Code:** 401 UNAUTHORIZED <br />  
	  
	* **Code:** 403 FORBIDDEN <br />  
  
	* **Code:** 40 BADREQUEST <br />  