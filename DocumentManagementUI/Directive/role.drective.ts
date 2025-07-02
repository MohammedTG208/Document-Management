import { Directive, inject, Input, TemplateRef, ViewContainerRef } from "@angular/core";

@Directive(
  {
    selector:'[role]'
  }
)
export class Role {
  private userRole = "user".toUpperCase();

  templateRef = inject(TemplateRef<any>);
  viewContainer = inject(ViewContainerRef);

  @Input() set appHasRole(allowedRoles: string[]) {
    if (allowedRoles.includes(this.userRole) || allowedRoles.includes('any'.toUpperCase())) {
      this.viewContainer.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainer.clear();
    }
  }
}
