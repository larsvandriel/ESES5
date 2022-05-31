FROM node:16-alpine as node-angular-cli

WORKDIR /app

ENV PATH /app/node_modules/.bin:$PATH

COPY package.json /app/package.json
RUN npm install --force
RUN npm install -g @angular/cli

EXPOSE 4200

COPY . /app

CMD ng serve --host 0.0.0.0
