# Use an official Node.js runtime as the base image
FROM node:14

# Set the working directory inside the container
WORKDIR /app

# Copy package.json and package-lock.json to the container
COPY package*.json ./

# Install dependencies
RUN npm install -g @angular/cli
RUN npm install

# Copy the rest of the application code to the container
COPY . .

# Expose the port that the Angular app will run on (default is 4200)
EXPOSE 4200

# Start the Angular app in the background using the "ng serve" command
CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "4200", "--disable-host-check"]
