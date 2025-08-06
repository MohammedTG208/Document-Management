import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { getHeaders } from '../../Auth/authService';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  private http=inject(HttpClient);
  headers=getHeaders();
  baseUrl=environment.apiUrl;
  constructor() { }

  getDocValue(uploadFile:any){
   const formData=new FormData();
    formData.append("formFile",uploadFile);
    return formData;
  }

  AddNewDocument(folderId:number|null, docName:string|null,formFile:FormData){
    return this.http.post(this.baseUrl+"/api/v1/Document/upload/"+folderId+"/"+docName,formFile,{headers:this.headers,responseType:'text'});
  }

  getDocumentbyId(folderId:number){
   return this.http.get<[any]>(this.baseUrl+"/api/v1/Document/"+folderId,{headers:this.headers});
  }

  getAllDocument(){
   return this.http.get<[any]>(this.baseUrl+"/api/v1/Document/get/all/doc");
  }

  downloadDocumentById(docId:number){
    return this.http.get(this.baseUrl+"/api/v1/Document/download/"+docId,{responseType: 'blob'});
  }

  deleteDocument(docId : number){
    return this.http.delete(this.baseUrl+"/api/v1/Document/delete/"+docId,{headers:this.headers, responseType:'text'});
  }
}
