**Retornar sistemas**
----
Retorna todos os sistemas cadastrados

* **URL**

  	/api/system

* **Method:**

   	`GET` 
  
*  **URL Params**

   	**Required:**
 
   	  Nenhum

* **Data Params**

  	Nenhum

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `[
    {
        "id": 1,
        "name": "VavilovSeeds"
    }]`
 
* **Error Response:**

  * **Code:** 401 UNAUTHORIZED <br />  
  
  * **Code:** 403 FORBIDDEN <br />

**Criar sistema**
----
  Cria um sistema para que seja possível relacionar os logs. Apenas a role Admin pode criar um sistema.

* **URL**

  	/api/system

* **Method:**

  	`POST` 
  
*  **URL Params**

   	**Required:**
 
   	Nenhum

* **Data Params**

	`name: text`  

* **Success Response:**
  

  * **Code:** 201 <br />
    **Content:** `{
    "id": 4,
    "name": "test"}`
 
* **Error Response:**
  
  * **Code:** 401 UNAUTHORIZED <br />  
  
  * **Code:** 403 FORBIDDEN <br />  
  
  * **Code:** 409 CONFLICT <br />  
      **Content**: `{ error = "User alredy exist!" }

**Atualizar sistema**
----
  Atualiza o nome de uma sistema. Apenas a role Admin pode fazer essa operação.

* **URL**

  	/api/system/:id

* **Method:**

	 `PUT`
  
*  **URL Params**

   **Required:**
 
   `id=[integer]`

* **Data Params**

  	`name: text`

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{  
	"id": 4,  
	"name": "test"}`
 
* **Error Response:**

 * **Code:** 400 BADREQUEST <br />  
	**Content**: `{ "System not found" }`  
  
 * **Code:** 401 UNAUTHORIZED <br />  
  
 * **Code:** 403 FORBIDDEN <br />  

**Deletar sistema**  
----  
Deleta um sistema.  Apenas a role Admin pode executar essa chamada.
  
* **URL**  
  
	/api/system/:id  
  
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
	**Content**: `{ error = "System not found" }`  

	* **Code:** 500 INTERNALSERVERERROR <br />  
	**Content**: `{ error = "Cannot delete system" }`  
  
