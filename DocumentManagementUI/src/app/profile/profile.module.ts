import { NgModule } from "@angular/core";
import { HeaderComponent } from "../header/header.component";
import { BrowserModule } from "@angular/platform-browser"; // Importing BrowserModule for browser-specific features }

@NgModule({
  declarations: [], // Declaring non-standalone components/directives ❌ Not for standalone
  imports: [HeaderComponent, BrowserModule], // Importing other modules or standalone components
  providers: [], //Providing services at the module level
  exports: [] // Sharing declared items with other modules
})
export class ProfileModule {
}
