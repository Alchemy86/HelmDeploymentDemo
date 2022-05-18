# HelmDeploymentDemo
Example deployment of a basic .net core API using HELM 3
** Requires an install of Kubernetes - Docker Desktop can enable this by default in settings to give you an instance.
** Requires HELM

## What is HELM

Helm is a package manager for Kubernetes. (k8s)
It helps to easily pack, configure and deploy applications and services onto Kubernetes.

Using Charts, a collection of files inside a directory, packages can be created for one line installations, upgrades and rollbacks.

## Installing HELM

Recommended with chocolatey, if using windows.
```
choco install kubernetes-helm
```
Other options: [Installation Options](https://helm.sh/docs/intro/install/)


## Create a Chart

```
helm create chart
```
##Chart.yaml
```
apiVersion: v2
name: test-app
description: A Helm chart for Kubernetes
 
type: application
 
version: 0.1.0
 
appVersion: "1.1.0"
```
<div style="
    margin: 2rem 0 2rem 0;
    font-weight: 400;
    border-style: solid;
    padding: 0.4rem 0.4rem 0.4rem 1rem;
    border-top: 1px solid #eee;
    border-bottom: 1px solid #eee;
    border-right: 1px solid #eee;
    border-left-width: calc(max(1em,15px));
    border-top-left-radius: calc(max(1em,15px));
    border-bottom-left-radius: calc(max(1em,15px));
    border-left-color: #f0ad4e;
">
<h1>
Helm will look for an image container with the tag the same as the appVersion
</h1>
</div>

## values.yaml
defines values for the app. Here you can define the name of the Image for the container. The configuration values for Service and Ingress for the app and many other things.

```
# Default values for test-app.
# This is a YAML-formatted file.
# Declare variables to be passed into your templates.

replicaCount: 1

image:
  repository: nginx
  pullPolicy: IfNotPresent
  # Overrides the image tag whose default is the chart appVersion.
  tag: ""

imagePullSecrets: []
nameOverride: ""
fullnameOverride: ""

serviceAccount:
  # Specifies whether a service account should be created
  create: true
  # Annotations to add to the service account
  annotations: {}
  # The name of the service account to use.
  # If not set and create is true, a name is generated using the fullname template
  name: ""

podAnnotations: {}

podSecurityContext: {}
  # fsGroup: 2000

securityContext: {}
  # capabilities:
  #   drop:
  #   - ALL
  # readOnlyRootFilesystem: true
  # runAsNonRoot: true
  # runAsUser: 1000

service:
  type: ClusterIP
  port: 80

ingress:
  enabled: false
  className: ""
  annotations: {}
    # kubernetes.io/ingress.class: nginx
    # kubernetes.io/tls-acme: "true"
  hosts:
    - host: chart-example.local
      paths:
        - path: /
          pathType: ImplementationSpecific
  tls: []
  #  - secretName: chart-example-tls
  #    hosts:
  #      - chart-example.local

resources: {}
  # We usually recommend not to specify default resources and to leave this as a conscious
  # choice for the user. This also increases chances charts run on environments with little
  # resources, such as Minikube. If you do want to specify resources, uncomment the following
  # lines, adjust them as necessary, and remove the curly braces after 'resources:'.
  # limits:
  #   cpu: 100m
  #   memory: 128Mi
  # requests:
  #   cpu: 100m
  #   memory: 128Mi

autoscaling:
  enabled: false
  minReplicas: 1
  maxReplicas: 100
  targetCPUUtilizationPercentage: 80
  # targetMemoryUtilizationPercentage: 80

nodeSelector: {}

tolerations: []

affinity: {}

```

Rename the repository to that of the build you are to create:

```
helmdemo
```

So when deployed, it will be looking for 'helmdemo:1.1.0' image if available. 

```angular2html
image:
  repository: helmdemo
  pullPolicy: IfNotPresent
```

The service is defined to be ClusterIP type and exposes it’s port 80 to other Kubernetes objects. ClusterIP service can only be accessed from inside the cluster. You can change it to NodePort to be opened on the browser using this service. Other options include 'LoadBalancer';

```
service:
  type: NodePort
  port: 80
```

## Build the docker image

```angular2html
 docker build -t helmdemo:1.1.0 -f .\HelmDeploymentDemo\Dockerfile .
```
Then in the helm chart folder, run the install command

```angular2html
helm install dockerhelmdemo .
```
See the pods
```angular2html
kubectl get pods
```
See the services
```angular2html
kubectl get services
```
This will show the port you can now hit the service on.
```angular2html
dockerhelmdemo2-hemldemo      NodePort    10.111.70.212    <none>        80:32352/TCP   6m48s
```
```angular2html
http://localhost:32352/
```
