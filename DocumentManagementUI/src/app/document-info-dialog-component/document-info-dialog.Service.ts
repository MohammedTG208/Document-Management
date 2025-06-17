import { Injectable, OutputEmitterRef } from "@angular/core";
import { Message } from "./add-message/document.model";

@Injectable({ providedIn: 'root' })
export class DocumentInfoDilogService {

  trackByIndex(index: number, item: any): number {
    console.log(index);
    return index;
  }

  AddMessage(tryAdd0: boolean): boolean {
    if (tryAdd0 == true) {
      tryAdd0 = false;
      return tryAdd0;
    } else {
      tryAdd0 = true;
      return tryAdd0;
    }

  }

  reseveNewMassage(newMassage: Message, document: {
    folderName: string;
    dateAdded: string;
    messages: Message[];}) {
    console.log('Received message:', newMassage);
    document.messages.push(newMassage);
  }

  close(notOpen: boolean): boolean {
    console.log('Close called with:', notOpen);
    return notOpen;
  }

}
