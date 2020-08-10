**Autenticar usuário**
----
  Requisitar o token jwt para o usuário

* **URL**

	/api/user/authenticate

* **Method:**

  	`GET`
  
*  **URL Params**

	**Required:**
 
   	Nenhum

* **Data Params**
	`name: text`  
	`password: text`

* **Success Response:**
  
  * **Code:** 200 <br />
    **Content:** `{
    "id": 1,
    "name": "admin",
    "token": "token"
}`
 
* **Error Response:**

  * **Code:** 400 BADREQUEST <br />  
	**Content**: `{ "User does not exist" }`  
	**Content**: `{ "User or password is incorrect" }`

**Criar usuário**
----
  Cria um usuário com uma determinada role. Apenas roles Admin podem criar usuários.

* **URL**

	api/user

* **Method:**

  	`POST`
  
*  **URL Params**

   	**Required:**
 
   	Nenhum

* **Data Params**
	`name: text`
	`role: text`,
	`password: text`

* **Success Response:**

  * **Code:** 201 <br />
    **Content:** `{
	"name": "vavilov",
	"role": "user",
	"password": "seeds"
}`
 
* **Error Response:**
 
	* **Code:** 400 BADREQUEST <br />  
	**Content**: `{ "Role does not exist" }`  
  
	* **Code:** 401 UNAUTHORIZED <br />  
  
	* **Code:** 403 FORBIDDEN <br />  
  
	* **Code:** 409 CONFLICT <br />  
	**Content**: `{ error = "User alredy exist!" }`

**Deletar Usuário**
----
  Deleta um usuário.

* **URL**

  	/api/user/:id

* **Method:**

  	`DELETE`
  
*  **URL Params**

   	**Required:**
 
   	`id=[integer]`

* **Data Params**
	Nenhum

* **Success Response:**
  

	* **Code:** 200 <br />

 
* **Error Response:**

	* **Code:** 400 BADREQUEST <br />  
		**Content**: `{ "Cannot delete user" }`  
  
	* **Code:** 401 UNAUTHORIZED <br />  
  
	* **Code:** 403 FORBIDDEN <br />  
  
	* **Code:** 404 NOTFOUND <br />  
		**Content**: `{ error = "User not found" }`

**Retornar todos os usuários**
----
  Retorna uma vetor json com todos os usuários. Apenas o Admin tem acesso a esse endpoint.

* **URL**

  	/api/user

* **Method:**

  	`GET`
  
*  **URL Params**

 	Nenhum

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `[
    {
        "id": 1,
        "name": "admin",
        "roleName": "admin"
    },
    {
        "id": 2,
        "name": "test",
        "roleName": "user"
    }]`
 
* **Error Response:**

  	* **Code:** 401 UNAUTHORIZED <br />

  	* **Code:** 403 FORBIDDEN <br />

**Retornar usuário**
----
Retorna o detalhe de um usuário

* **URL**

  	/api/user/:id

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
    "name": "test",
    "roleName": "user"
	}`
 
* **Error Response:**

	* **Code:** 400 BADREQUEST <br />
		**Content**: `{ "Your role cannot see other user data"  }`

	* **Code:** 401 UNAUTHORIZED <br />  
  
	* **Code:** 403 FORBIDDEN <br />
  
	* **Code:** 404 NOTFOUND <br />
		**Content**: `{ error = "User not found" }`

**Atualizar senha do usuário**  
----  
Atualiza a senha de um usuário.
  
* **URL**  
  
	/api/user/password/:id
  
* **Method:**  
  
	`PUT`  
  
* **URL Params**  
  
	**Required:**  
  
	`id=[integer]`
  
* **Data Params**  
  
	`name: text`  
	`password: text`
  
* **Success Response:**  
  
* **Code:** 200 <br />  
	**Content:** `{  
	"id": 2,  
	"name": "test",  
	"roleName": "user"  
	}`  
  
* **Error Response:**  
  
	* **Code:** 400 BADREQUEST <br />  
	**Content**: `{ "Cannot update password from another user" }`  
  
	* **Code:** 401 UNAUTHORIZED <br />  
  
	* **Code:** 403 FORBIDDEN <br />  
  
	* **Code:** 404 NOTFOUND <br />  
	**Content**: `{ error = "User not found" }`  
  
**Atualiza role do usuário**  
----  
Atualiza a role de um usuário 
  
* **URL**  
  
	/api/user/role
  
* **Method:**  
  
	`PUT`  
  
* **URL Params**  
  
	**Required:**  
  
	Nenhum
  
* **Data Params**  
  
	`name: text`  
	`roleName: text`
  
* **Success Response:**  
  
	* **Code:** 200 <br />  
	**Content:** `{  
	"id": 2,  
	"name": "test",  
	"roleName": "user"  
	}`  
  
* **Error Response:**  
  
	* **Code:** 400 BADREQUEST <br />  
	**Content**: `{ "Role does not exist" }`  
  
	* **Code:** 401 UNAUTHORIZED <br />  
  
	* **Code:** 403 FORBIDDEN <br />  
  
	* **Code:** 404 NOTFOUND <br />  
	**Content**: `{ error = "User not found" }`