#
cd src
#
sudo dotnet workload restore
#
dotnet dev-certs https --trust
#
echo "Setup complete"