import { Injectable } from '@angular/core';  
import { Http, Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';  
import { Observable } from 'rxjs/'; 

import 'rxjs/add/operator/map';
@Injectable()

export class Counterservice {
  
  constructor(private http: HttpClient)  
    {  
  
    }

    getItems(){  
      let apiUrl = 'api/SampleData';  
      return this.http.get(apiUrl)  
                 .map((res: Response) => {return res.json()  
      });  
  }

  postfile(selectedfile:File)
  {
      const fd = new FormData();
      fd.append('video',selectedfile,selectedfile.name);
      let apiUrl = 'api/Upload/SaveFile';
      console.log('before calling http post webapi');
      this.http.post(apiUrl,fd).subscribe(res=>{console.log(res)});
      console.log('after calling http post webapi');
      
  }


    
}