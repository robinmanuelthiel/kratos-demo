import KratosMessage from "./KratosMessage";

export default class KratosFormField {
  name = '';
  type = '';
  required = false;
  value = '';
  messages: KratosMessage[] = [];
}
