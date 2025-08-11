import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { getHeaders, getToken } from '../../Auth/authService';
import { MessageDto } from './message.component';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private http=inject(HttpClient);
  baseUrl=environment.apiUrl;
  header=getHeaders()

  getDocumentMessage(docid:number){
    return this.http.get<any[]>(this.baseUrl+"/api/v1/Document/getMessages/"+docid,{headers:this.header});
  }

  addMessageToDucment(docid:number, message:{content:string,isPublic:boolean}){
    return this.http.post(this.baseUrl+"/api/v1/Message/add/"+docid,message,{headers:this.header,responseType:'text'});
  }
}
