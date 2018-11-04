**Atividades de código**

- Submeter código apenas para a branch develop.

- Caso surja um bug em **produção** e precise ser corrigido e publicado, corrigir na branch **hotfix** (caso não exista, deve-se criar tal branch), ao final do ciclo de hotfix, efetuar o deploy e devolver a correção por merge request para **master** e depois para **develop**

- Iniciar sempre uma atividade relacionada a uma Issue no GitLab. Caso não exista, deve-se criar a Issue e solicitar a um Analista que complete as informações da Issue;

- Definir estimativa de tempo usando o comando **/estimate** do GitLab (Ex.: /estimate 1h 30m);

- Trabalhar na Issue interagindo nos comentários quando necessário a fim de manter o histórico dos acontecimentos relacionados;

- Ao terminar, registrar o tempo gasto na Issue com o comanto **/spend** do GitLab(Ex.: /spend 1h 15m)

**Ao abrir uma nova Issue**

- Identificar o tipo;
- Identificar o ambiente;

**Quanto ao tipo:**
- Caso seja um problema que interrompa ou prejudique o fluxo de negócio, adicionar o label **Bug**;
- Caso seja algo que está diferente da documentação ou incorreto porém não prejudica o fluxo de negócio, adicionar o label **Ajuste**;
- Caso seja algo a ser adicionado ou alterado em uma funcionalidade existente, adicionar o label **Melhoria**
- Caso seja uma funcionalidade completamente nova, adicionar o label **Implementação**;

**Quanto ao ambiente**
- Caso seja um **Bug** ou **Ajuste** em produção, adicionar o label **Ambiente de Produção**
- Caso seja um **Bug** ou **Ajuste** em homologação, adicionar o label **Ambiente de Homologação**
- Caso seja um **Bug** ou **Ajuste** em desenvolvimento, não adicionar label de ambiente;

Caso seja uma **Melhoria**, não adicionar label de ambiente pois todas as melhorias são tratadas na esteira padrão de desenvolvimento.