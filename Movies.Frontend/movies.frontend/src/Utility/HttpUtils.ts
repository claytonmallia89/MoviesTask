import Config from "./Config";

// #region private functions
 function getHeaders(): Headers {
	const headers: Headers = new Headers()
	// Add headers
	headers.set('Content-Type', 'application/json')
	headers.set('Accept', 'application/json')
	headers.set('Access-Control-Allow-Origin', '*')

	//would add authorization token here
	return headers;
   }
  
 function getRequestInfo(url:string,params:URLSearchParams): RequestInfo {
	
	const headers: Headers = getHeaders();
  
	let apiUrl = new URL(`${Config.baseUrl}${url}`);
  
	if(params.size > 0)
	{
		apiUrl.search = params.toString();
	}
  
	const request: RequestInfo = new Request(apiUrl, {
	  method: 'GET',
	  headers: headers
	});
  
	return request;
}
// #endregion

//#region http method functions
   export function get<T>(resourceName:string,params:URLSearchParams = new URLSearchParams()): Promise<T> {
    const request: RequestInfo = getRequestInfo(resourceName,params);
    return fetch(request)
      .then(res =>  res.json())
      .then(res => {
        return res as T
      });
    } 

	/*TODO: to add methods for:
		-post
		-put
		-patch
		-delete
	 */
//#endregion